using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.Common;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly string connectionString = "Server=192.168.51.210;Database=audios_dana;User=root;Password=;";

    [HttpGet("getUserInfo")]
    public async Task<IActionResult> GetUserInfo(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("El nombre es requerido.");
        }

        var user = new
        {
            Email = "",
            User = "",
            Name = "",
            Rol = "",
            Staff = ""
        };

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            await conn.OpenAsync();
            string selectQuery = "SELECT email, user, name, rol, staff FROM users WHERE name = @Name";
            MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
            selectCmd.Parameters.AddWithValue("@Name", name);

            using (DbDataReader reader = await selectCmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    user = new
                    {
                        Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                        User = reader.IsDBNull(reader.GetOrdinal("user")) ? null : reader.GetString(reader.GetOrdinal("user")),
                        Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                        Rol = reader.IsDBNull(reader.GetOrdinal("rol")) ? null : reader.GetString(reader.GetOrdinal("rol")),
                        Staff = reader.IsDBNull(reader.GetOrdinal("staff")) ? null : reader.GetString(reader.GetOrdinal("staff"))
                    };
                }
                else
                {
                    return NotFound("Usuario no encontrado.");
                }
            }
        }

        return Ok(user);
    }
}
