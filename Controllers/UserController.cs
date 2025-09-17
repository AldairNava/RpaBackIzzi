using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string connectionString = "Server=192.168.51.210;Database=audios_dana;User=root;Password=thor;";

        [HttpGet("permisos/{role}")]
        public async Task<IActionResult> GetPermisos(string role)
        {
            var permisos = new List<string>();

            using (var conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                const string query = @"
                    SELECT p.key_permiso
                    FROM roles_permisos rp
                    INNER JOIN rolescx r   ON rp.role_id    = r.id
                    INNER JOIN permisoscx p ON rp.permiso_id = p.id
                    WHERE r.name = @Role;";

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Role", role);

                using var reader = await cmd.ExecuteReaderAsync();
                int idxKey = reader.GetOrdinal("key_permiso");

                while (await reader.ReadAsync())
                {
                    permisos.Add(reader.GetString(idxKey));
                }
            }

            if (!permisos.Any())
                return NotFound(new { message = $"Rol '{role}' no existe o no tiene permisos." });

            return Ok(permisos);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = new List<RoleDto>();
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"SELECT name, area, departamento, staff, status FROM rolescx;";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                roles.Add(new RoleDto
                {
                    Name = reader.GetString(0),
                    Area = reader.GetString(1),
                    Departamento = reader.GetString(2),
                    Staff = reader.GetString(3),
                    Status = reader.GetInt32(4)
                });
            }

            return Ok(roles);
        }

        [HttpGet("getRole")]
        public async Task<IActionResult> GetRole()
        {
            var roles = new List<object>();
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"SELECT departamento, name FROM rolescx WHERE status = 1;";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                roles.Add(new
                {
                    Departamento = reader.GetString(0),
                    Name = reader.GetString(1)
                });
            }

            return Ok(roles);
        }

        [HttpGet("permisos")]
        public async Task<IActionResult> GetPermisosGlobales()
        {
            var permisos = new List<string>();
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"SELECT key_permiso, area, departamento, staff FROM permisoscx;";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                // Si deseas retornar objetos con metadata, crea un DTO nuevo
                permisos.Add(reader.GetString(0));
            }

            return Ok(permisos);
        }

        [HttpPut("AsignarPermisos")]
        public async Task<IActionResult> AsignarPermisos([FromBody] JsonObject info)
        {
            var role = info["Role"]?.ToString()?.Trim();
            var permsArr = info["Permisos"] as JsonArray;

            if (string.IsNullOrWhiteSpace(role))
                return BadRequest("La propiedad 'Role' es obligatoria.");
            if (permsArr == null)
                return BadRequest("La propiedad 'Permisos' debe ser un arreglo de cadenas.");

            var permisos = permsArr
                .Select(x => x?.ToString()?.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();

            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            using var tx = await conn.BeginTransactionAsync();

            try
            {
                // 1) Obtener el role_id
                int roleId;
                const string getRoleSql = "SELECT id FROM rolescx WHERE name = @Role;";
                using (var getRoleCmd = new MySqlCommand(getRoleSql, conn, tx))
                {
                    getRoleCmd.Parameters.AddWithValue("@Role", role);
                    var result = await getRoleCmd.ExecuteScalarAsync();
                    if (result == null)
                        return NotFound(new { message = $"Rol '{role}' no existe." });
                    roleId = Convert.ToInt32(result);
                }

                // 2) Borrar asignaciones antiguas
                const string deleteSql = "DELETE FROM roles_permisos WHERE role_id = @RoleId;";
                using (var delCmd = new MySqlCommand(deleteSql, conn, tx))
                {
                    delCmd.Parameters.AddWithValue("@RoleId", roleId);
                    await delCmd.ExecuteNonQueryAsync();
                }

                // 3) Insertar nuevas asignaciones
                const string insertSql = @"
                INSERT INTO roles_permisos (role_id, permiso_id)
                SELECT @RoleId, p.id
                  FROM permisoscx p
                 WHERE p.key_permiso = @KeyPermiso;";
                using var insCmd = new MySqlCommand(insertSql, conn, tx);
                insCmd.Parameters.AddWithValue("@RoleId", roleId);
                insCmd.Parameters.Add("@KeyPermiso", MySqlDbType.VarChar);

                foreach (var key in permisos)
                {
                    insCmd.Parameters["@KeyPermiso"].Value = key;
                    await insCmd.ExecuteNonQueryAsync();
                }

                await tx.CommitAsync();
                return Ok(new { message = $"Se asignaron {permisos.Count} permisos al rol '{role}'." });
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                Console.Error.WriteLine("Error en AsignarPermisos: " + ex);
                return StatusCode(500, $"Error asignando permisos: {ex.Message}");
            }
        }

        [HttpPut("actualizarRole")]
        public async Task<IActionResult> ActualizarRole([FromBody] JsonObject info)
        {
            var role = info["Role"]?.ToString()?.Trim();
            var newName = info["NewRole"]?.ToString()?.Trim() ?? role;
            var area = info["Area"]?.ToString() ?? string.Empty;
            var dept = info["Departamento"]?.ToString() ?? string.Empty;
            var staff = info["Staff"]?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(role))
                return BadRequest("La propiedad 'Role' (nombre actual) es obligatoria.");

            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"
            UPDATE rolescx
               SET name        = @NewName,
                   area        = @Area,
                   departamento= @Departamento,
                   staff       = @Staff
             WHERE name        = @Role;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NewName", newName);
            cmd.Parameters.AddWithValue("@Area", area);
            cmd.Parameters.AddWithValue("@Departamento", dept);
            cmd.Parameters.AddWithValue("@Staff", staff);
            cmd.Parameters.AddWithValue("@Role", role);

            var affected = await cmd.ExecuteNonQueryAsync();
            if (affected == 0)
                return NotFound(new { message = $"Rol '{role}' no encontrado." });

            return Ok(new { message = $"Rol '{role}' actualizado correctamente." });
        }

        [HttpGet("getUsersCx")]
        public async Task<IActionResult> GetUsersCx()
        {
            var users = new List<UserDto>();

            const string sql = @"
        SELECT 
            id_users,
            `user`   AS Username,
            email,
            active,
            name,
            rol,
            staff,
            area,
            departamento
        FROM users;";

            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            int idxId = reader.GetOrdinal("id_users");
            int idxUser = reader.GetOrdinal("Username");
            int idxEmail = reader.GetOrdinal("email");
            int idxActive = reader.GetOrdinal("active");
            int idxName = reader.GetOrdinal("name");
            int idxRol = reader.GetOrdinal("rol");
            int idxStaff = reader.GetOrdinal("staff");
            int idxArea = reader.GetOrdinal("area");
            int idxDepartamento = reader.GetOrdinal("departamento");

            while (await reader.ReadAsync())
            {
                users.Add(new UserDto
                {
                    id_users = reader.IsDBNull(idxId) ? 0 : reader.GetInt32(idxId),
                    Username = reader.IsDBNull(idxUser) ? null : reader.GetString(idxUser),
                    Email = reader.IsDBNull(idxEmail) ? null : reader.GetString(idxEmail),
                    Active = reader.IsDBNull(idxActive) ? (bool?)null : reader.GetBoolean(idxActive),
                    Name = reader.IsDBNull(idxName) ? null : reader.GetString(idxName),
                    Rol = reader.IsDBNull(idxRol) ? null : reader.GetString(idxRol),
                    Staff = reader.IsDBNull(idxStaff) ? null : reader.GetString(idxStaff),
                    Area = reader.IsDBNull(idxArea) ? null : reader.GetString(idxArea),
                    Departamento = reader.IsDBNull(idxDepartamento) ? null : reader.GetString(idxDepartamento)
                });
            }

            if (!users.Any())
                return NotFound(new { message = "No se encontraron usuarios." });

            return Ok(users);
        }

        [HttpPut("ActualizarUsuarioCx")]
        public async Task<IActionResult> ActualizarUsuarioCx(int id, [FromBody] UsuarioCxUpdateDto dto)
        {
            if (id != dto.id_users)
                return BadRequest("El id no coincide.");

            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            using var tx = await conn.BeginTransactionAsync();

            try
            {
                const string sql = @"
            UPDATE users
               SET `user`        = @User,
                   email         = @Email,
                   pass          = @Pass,
                   active        = @Active,
                   name          = @Name,
                   rol           = @Rol,
                   staff         = @Staff,
                   area          = @Area,
                   departamento  = @Departamento
             WHERE id_users      = @Id;";

                using var cmd = new MySqlCommand(sql, conn, tx);
                cmd.Parameters.AddWithValue("@User", dto.user);
                cmd.Parameters.AddWithValue("@Email", dto.email);
                cmd.Parameters.AddWithValue("@Pass", dto.pass);
                cmd.Parameters.AddWithValue("@Active", dto.active);
                cmd.Parameters.AddWithValue("@Name", dto.name);
                cmd.Parameters.AddWithValue("@Rol", dto.rol);
                cmd.Parameters.AddWithValue("@Staff", dto.staff);
                cmd.Parameters.AddWithValue("@Area", dto.area);
                cmd.Parameters.AddWithValue("@Departamento", dto.departamento);
                cmd.Parameters.AddWithValue("@Id", dto.id_users);

                var afectadas = await cmd.ExecuteNonQueryAsync();
                if (afectadas == 0)
                    return NotFound("Usuario no encontrado.");

                await tx.CommitAsync();
                return Ok(new { message = "Usuario actualizado correctamente." });
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] JsonObject info)
        {
            if (info == null)
                return BadRequest("El objeto JSON no puede ser nulo.");

            // Extraemos los campos del objeto
            var role = info["Role"]?.ToString()?.Trim();
            var area = info["Area"]?.ToString() ?? string.Empty;
            var dept = info["Departamento"]?.ToString() ?? string.Empty;
            var staff = info["Staff"]?.ToString() ?? string.Empty;

            Console.WriteLine($"DTO recibido → Role: {role}, Area: {area}, Departamento: {dept}, Staff: {staff}");

            if (string.IsNullOrWhiteSpace(role))
                return BadRequest("La propiedad 'Role' es obligatoria.");

            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"
            INSERT INTO rolescx
              (name, area, departamento, staff, status)
            VALUES
              (@Role, @Area, @Departamento, @Staff, 0);";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Role", role);
            cmd.Parameters.AddWithValue("@Area", area);
            cmd.Parameters.AddWithValue("@Departamento", dept);
            cmd.Parameters.AddWithValue("@Staff", staff);

            try
            {
                await cmd.ExecuteNonQueryAsync();
                return CreatedAtAction(nameof(GetRoles),
                    new { role = role },
                    new { message = $"Rol '{role}' creado correctamente." });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error al insertar rol: " + ex);
                return StatusCode(500, $"Error interno al crear el rol: {ex.Message}");
            }
        }

        [HttpDelete("roles/{role}")]
        public async Task<IActionResult> DeleteRole(string role)
        {
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"
                UPDATE rolescx
                   SET status = 0
                 WHERE name = @Role;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Role", role);
            var affected = await cmd.ExecuteNonQueryAsync();
            if (affected == 0)
                return NotFound(new { message = $"Rol '{role}' no encontrado." });

            return NoContent();
        }

        [HttpPost("permisos")]
        public async Task<IActionResult> CreatePermiso([FromBody] PermisoCreateDto dto)
        {
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            const string sql = @"
                INSERT INTO permisoscx (key_permiso, area, departamento, staff)
                VALUES (@Key, @Area, @Departamento, @Staff);";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Key", dto.Key_Permiso);
            cmd.Parameters.AddWithValue("@Area", dto.Area);
            cmd.Parameters.AddWithValue("@Departamento", dto.Departamento);
            cmd.Parameters.AddWithValue("@Staff", dto.Staff);
            await cmd.ExecuteNonQueryAsync();

            return CreatedAtAction(nameof(GetPermisosGlobales), null, new { message = $"Permiso '{dto.Key_Permiso}' creado." });
        }

        [HttpDelete("permisos/{perm}")]
        public async Task<IActionResult> DeletePermiso(string perm)
        {
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            const string sql = "DELETE FROM permisoscx WHERE key_permiso = @Key;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Key", perm);
            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows == 0)
                return NotFound(new { message = $"Permiso '{perm}' no encontrado." });

            return NoContent();
        }

        [HttpPut("roles/{role}/status")]
        public async Task<IActionResult> SetRoleStatus(string role, [FromQuery] bool active)
        {
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            const string sql = @"
                UPDATE rolescx
                   SET status = @Status
                 WHERE name   = @Role;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", active ? 1 : 0);
            cmd.Parameters.AddWithValue("@Role", role);

            var affected = await cmd.ExecuteNonQueryAsync();
            if (affected == 0)
                return NotFound(new { message = $"Rol '{role}' no encontrado." });

            return NoContent();
        }
    }
}