using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Verifyuser : ControllerBase
    {
        private readonly string connectionString = "Server=192.168.51.210;Database=audios_dana;User=root;Password=;";

        [HttpPost("verifyUser")]
        public async Task<IActionResult> VerifyUser([FromBody] string pass)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT id_users, user, email, pass, active, name, rol, staff FROM users WHERE pass = @pass";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pass", pass);

                DbDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var user = new
                    {
                        Id = reader["id_users"],
                        Username = reader["user"],
                        Email = reader["email"],
                        Pass = reader["pass"],
                        Active = reader["active"],
                        Name = reader["name"],
                        Role = reader["rol"],
                        Staff = reader["staff"]
                    };

                    return Redirect($"https://frontrpaizzi.azurewebsites.net/mariana?user={System.Web.HttpUtility.UrlEncode(System.Text.Json.JsonSerializer.Serialize(user))}");
                }

                return Unauthorized("Unauthorized");
            }
        }
    }
}
