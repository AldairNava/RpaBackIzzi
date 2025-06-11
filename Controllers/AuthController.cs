using ITfoxtec.Identity.Saml2.Schemas;
using ITfoxtec.Identity.Saml2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sustainsys.Saml2.AspNetCore2;
using System.Security.Authentication;
using System.Security.Claims;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("insetar usuario")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllProcess()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("INSERT INTO Usuarios (Nombre, Correo, Rol, Otros) VALUES ('Prueba', 'prueba@correo.com', 'Usuario', 'Otros datos')");


                var datos = await _context.Usuarios.ToListAsync();

                if (datos.Count > 0)
                {
                    return Ok(datos);
                }
                else
                {
                    // Si no hay datos, devuelve "SIN INFO"
                    var d = new List<string>()
            {
                "SIN INFO"
            };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // En caso de error, devuelve el mensaje de error
            }
        }


        [HttpGet]
        [Route("ObtenerPendientes")]
        public async Task<IActionResult> ObtenerPendientes()
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();

                await using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_ObtenerPendientes";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 240;

                    // Listas para acumulación de resultados
                    var listaPendientes = new List<object>();
                    var listaUltimos = new List<object>();

                    // Ejecutamos y leemos el primer result set: Pendientes por tabla
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaPendientes.Add(new
                            {
                                Tabla = reader["Tabla"]?.ToString(),
                                Pendientes = reader["Pendientes"] != DBNull.Value
                                             ? Convert.ToInt32(reader["Pendientes"])
                                             : 0
                            });
                        }

                        // Pasamos al segundo result set: Ultimos por tabla e IP
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                listaUltimos.Add(new
                                {
                                    Tabla = reader["Tabla"]?.ToString(),
                                    IP = reader["IP"]?.ToString(),
                                    UltimaFechaCompletado = reader["UltimaFechaCompletado"] != DBNull.Value
                                                             ? (DateTime?)reader["UltimaFechaCompletado"]
                                                             : null
                                });
                            }
                        }
                    }

                    // Si no hay datos en ninguno de los dos...
                    if (!listaPendientes.Any() && !listaUltimos.Any())
                        return Ok("SIN INFO");

                    // Retornamos un objeto con ambas listas
                    return Ok(new
                    {
                        pendientes = listaPendientes,
                        ultimos = listaUltimos
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                var state = _context.Database.GetDbConnection().State;
                if (state == System.Data.ConnectionState.Open)
                    await _context.Database.GetDbConnection().CloseAsync();
            }
        }


    }
}