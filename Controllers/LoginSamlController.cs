using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginSamlController : Controller
    {
        private readonly string connectionString = "Server=192.168.51.210;Database=audios_dana;User=root;Password=thor;";

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/LoginSaml/LoginCallback" }, "Saml2");
        }

        [HttpGet("LoginCallback")]
        public async Task<IActionResult> LoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync("external");
            if (result.Succeeded && result.Principal != null)
            {
                var claims = result.Principal.Identities.First().Claims;
                var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(name))
                    return Unauthorized("No se encontró el nombre en los claims.");

                string token = Guid.NewGuid().ToString();

                using (var conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string selectQuery = @"SELECT user FROM users WHERE email = @Email";
                    using var selectCmd = new MySqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@Email", name);

                    bool usuarioExiste = false;

                    using (var reader = await selectCmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuarioExiste = true;
                        }
                        // No necesitas llamar a reader.Close(); el using lo hace automáticamente
                    }

                    if (usuarioExiste)
                    {
                        string updateQuery = @"UPDATE users SET pass = @Token WHERE email = @Email";
                        using var updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@Token", token);
                        updateCmd.Parameters.AddWithValue("@Email", name);
                        await updateCmd.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        string insertQuery = @"
                            INSERT INTO users
                              (`user`, email, pass, active, name, rol, staff, area, departamento)
                            VALUES
                              (@User, @Email, @Pass, @Active, @Name, @Rol, @Staff, @Area, @Departamento)";
                        using var insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@User", name.Split('@')[0]);
                        insertCmd.Parameters.AddWithValue("@Email", name);
                        insertCmd.Parameters.AddWithValue("@Pass", token);
                        insertCmd.Parameters.AddWithValue("@Active", true);
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        insertCmd.Parameters.AddWithValue("@Rol", "General");
                        insertCmd.Parameters.AddWithValue("@Staff", "General");
                        insertCmd.Parameters.AddWithValue("@Area", "General");
                        insertCmd.Parameters.AddWithValue("@Departamento", "general");
                        await insertCmd.ExecuteNonQueryAsync();
                    }
                }

                var url = $"https://frontrpaizzi.azurewebsites.net/verify?token={token}";
                return Redirect(url);
            }

            return Unauthorized();
        }

        [HttpGet("session")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSession([FromQuery] string token)
        {
            // Limpia el token si viene con un ? al final
            if (!string.IsNullOrEmpty(token) && token.EndsWith("?"))
                token = token.Substring(0, token.Length - 1);

            using (var conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var transaction = await conn.BeginTransactionAsync();

                object userData = null;

                string selectQuery = @"
            SELECT user, email, name, rol, staff, area, departamento
            FROM users
            WHERE pass = @Token";
                using (var selectCmd = new MySqlCommand(selectQuery, conn, (MySqlTransaction)transaction))
                {
                    selectCmd.Parameters.AddWithValue("@Token", token);

                    using (var reader = await selectCmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userData = new
                            {
                                User = reader.IsDBNull(reader.GetOrdinal("user")) ? null : reader.GetString(reader.GetOrdinal("user")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                                Role = reader.IsDBNull(reader.GetOrdinal("rol")) ? null : reader.GetString(reader.GetOrdinal("rol")),
                                Staff = reader.IsDBNull(reader.GetOrdinal("staff")) ? null : reader.GetString(reader.GetOrdinal("staff")),
                                Area = reader.IsDBNull(reader.GetOrdinal("area")) ? null : reader.GetString(reader.GetOrdinal("area")),
                                Departamento = reader.IsDBNull(reader.GetOrdinal("departamento")) ? null : reader.GetString(reader.GetOrdinal("departamento"))
                            };
                        }
                    }
                }

                if (userData != null)
                {
                    string updateQuery = @"UPDATE users SET pass = NULL WHERE pass = @Token";
                    using var updateCmd = new MySqlCommand(updateQuery, conn, (MySqlTransaction)transaction);
                    updateCmd.Parameters.AddWithValue("@Token", token);
                    await updateCmd.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                    return Ok(userData);
                }
                else
                {
                    await transaction.RollbackAsync();
                    return Unauthorized();
                }
            }
        }

    }
}
