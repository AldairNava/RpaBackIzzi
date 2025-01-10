using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Linq;
using WebApplication1.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginSamlController : Controller
    {
        private readonly string connectionString = "Server=192.168.51.210;Database=audios_dana;User=root;Password=;";

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
                {
                    // Manejar el caso de name nulo
                    return Unauthorized("No se encontró el nombre en los claims.");
                }

                var userExists = false;
                var userData = new
                {
                    User = "",
                    Email = "",
                    Name = "",
                    Role = ""
                };

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string selectQuery = "SELECT user, email, name, rol FROM users WHERE email = @Name";
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@Name", name);

                    using (DbDataReader reader = await selectCmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userExists = true;
                            userData = new
                            {
                                User = reader.IsDBNull(reader.GetOrdinal("user")) ? null : reader.GetString(reader.GetOrdinal("user")),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                                Role = reader.IsDBNull(reader.GetOrdinal("rol")) ? null : reader.GetString(reader.GetOrdinal("rol"))
                            };
                        }
                    }
                }

                if (!userExists)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        await conn.OpenAsync();
                        string insertQuery = "INSERT INTO users (user, email, name, rol, staff) VALUES (@User, @Email, @Name, @Rol, @Staff)";
                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@User", name.Split('@')[0]);
                        insertCmd.Parameters.AddWithValue("@Email", name);
                        insertCmd.Parameters.AddWithValue("@Name", name);
                        insertCmd.Parameters.AddWithValue("@Rol", "general");
                        insertCmd.Parameters.AddWithValue("@Staff", name.Split('@')[1].Split('.')[0]);

                        await insertCmd.ExecuteNonQueryAsync();
                    }

                    // Volver a hacer la consulta para obtener los valores insertados
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        await conn.OpenAsync();
                        string selectQuery = "SELECT user, email, name, rol FROM users WHERE email = @Name";
                        MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                        selectCmd.Parameters.AddWithValue("@Name", name);

                        using (DbDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                userData = new
                                {
                                    User = reader.IsDBNull(reader.GetOrdinal("user")) ? null : reader.GetString(reader.GetOrdinal("user")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                                    Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
                                    Role = reader.IsDBNull(reader.GetOrdinal("rol")) ? null : reader.GetString(reader.GetOrdinal("rol"))
                                };
                            }
                        }
                    }
                }

                // Redirigir con el objeto JSON del usuario
                return Redirect($"https://frontrpaizzi.azurewebsites.net/verify?user={System.Web.HttpUtility.UrlEncode(System.Text.Json.JsonSerializer.Serialize(userData))}");
            }

            return Unauthorized();
        }
    }
}
