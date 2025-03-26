using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class okclienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        public okclienteController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [Route("InsertarBasesOkCliente")]
        [HttpPost]
        public dynamic InsertarBasesOkCliente([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            // Lista para almacenar las cuentas duplicadas
            List<string> cuentasDuplicadas = new List<string>();

            try
            {
                conn.Open();
                foreach (var item in Info)
                {
                    var data = (JsonObject)item;
                    string cuenta = data["Cuenta"]?.ToString() ?? "";

                    // Verifica si existe un registro con la misma cuenta y fecha (ajustando la zona horaria)
                    string checkSql = "SELECT COUNT(*) FROM okcliente WHERE Cuenta = @Cuenta AND CONVERT(date, FechaCaptura) = CONVERT(date, DATEADD(hour, -6, GETDATE()))";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@Cuenta", cuenta);
                    int registroExistente = (int)checkCmd.ExecuteScalar();

                    if (registroExistente > 0)
                    {
                        // Si ya existe, se añade el número de cuenta a la lista de duplicados
                        cuentasDuplicadas.Add(cuenta);
                    }
                    else
                    {
                        // Inserta el registro si no es duplicado
                        string sql = "INSERT INTO okcliente (FechaCaptura, Status, Cve_usuario, Ip, Cuenta, numeroOrden, Hub, TipoOferta, FechaEncuesta, Nombre, Telefono) " +
                                     "VALUES (@FechaCaptura, @Status, @Cve_usuario, @Ip, @Cuenta, @numeroOrden, @Hub, @TipoOferta, @FechaEncuesta, @Nombre, @Telefono)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@FechaCaptura", data["FechaCaptura"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", data["Status"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cve_usuario", data["Cve_usuario"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ip", data["Ip"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                        cmd.Parameters.AddWithValue("@numeroOrden", data["numeroOrden"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Hub", data["Hub"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoOferta", data["TipoOferta"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaEncuesta", data["FechaEncuesta"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Nombre", data["Nombre"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", data["Telefono"]?.ToString() ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();

                // Retorna información sobre los registros procesados y duplicados
                return Ok(new
                {
                    message = "Registros procesados",
                    hayDuplicados = cuentasDuplicadas.Count > 0,
                    cuentasDuplicadas = cuentasDuplicadas
                });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }



        [Route("getStatsOkCliente")]
        [HttpGet]
        public async Task<IActionResult> getStatsOkCliente()
        {
            var date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            string connectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            string sql = @"
                            SELECT 
                                SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,
                                SUM(CASE 
                                    WHEN Status LIKE '%Error%'  
                                    THEN 1 ELSE 0 END) AS Error,
                                SUM(CASE 
                                    WHEN Status = 'Cerrado'  
                                    THEN 1 ELSE 0 END) AS Completado,
                                SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,
                                COUNT(*) AS Total
                            FROM okcliente 
                            WHERE CONVERT(date, FechaCaptura) BETWEEN @StartDate AND @EndDate";

            var result = new List<object>();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", date);
                        cmd.Parameters.AddWithValue("@EndDate", date);

                        await conn.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                result.Add(new
                                {
                                    Pendientes = reader.GetInt32(reader.GetOrdinal("Pendientes")),
                                    Error = reader.GetInt32(reader.GetOrdinal("Error")),
                                    Completado = reader.GetInt32(reader.GetOrdinal("Completado")),
                                    Procesando = reader.GetInt32(reader.GetOrdinal("Procesando")),
                                    Total = reader.GetInt32(reader.GetOrdinal("Total"))
                                });
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("ActualizaOkCliente")]
        [HttpPut]
        public dynamic ActualizaOkCliente(int id, [FromBody] okcliente Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    Cuenta.FechaCompletado = DateTime.Now;
                    _context.Update(Cuenta);
                    _context.SaveChanges();
                    return Ok(Cuenta);

                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAllOkCliente")]
        public async Task<ActionResult<IEnumerable<okcliente>>> getAllOkCliente()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.okcliente.FromSqlRaw($" select * from okcliente where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
                if (datos.Count() > 0)
                {
                    return Ok(datos);

                }
                else
                {
                    var d = new List<string>()
                    {
                        "SIN INFO"
                    };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCuentaOkCliente")]
        public async Task<ActionResult<IEnumerable<okcliente>>> getCuentaOkCliente()
        {
            try
            {
                var datos = _context.okcliente.FromSqlRaw("exec Sp_getCuentaokcliente").ToList();
                if (datos.Count() > 0)
                {
                    return Ok(datos);

                }
                else
                {
                    var d = new List<string>()
                    {
                        "SIN INFO"
                    };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("InsertarBasesOkClienteRPA")]
        [HttpPost]
        public dynamic InsertarBasesOkClienteRPA([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var item in Info)
                {
                    var data = (JsonObject)item;

                    string encuesta = data["Encuesta"]?.ToString() ?? "";
                    if (encuesta.Trim().ToLower() == "si" || encuesta.Trim() == "1")
                    {
                        string checkSql = "SELECT COUNT(*) FROM okcliente WHERE Cuenta = @Cuenta AND CONVERT(date, FechaCaptura) = CONVERT(date, DATEADD(hour, -6, GETDATE()))";
                        SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                        checkCmd.Parameters.AddWithValue("@Cuenta", data["Cuenta"]?.ToString() ?? (object)DBNull.Value);
                        int registroExistente = (int)checkCmd.ExecuteScalar();

                        if (registroExistente == 0)
                        {
                            string sql = "INSERT INTO okcliente (FechaCaptura, Status, Cve_usuario, Ip, Cuenta, numeroOrden, Hub, TipoOferta, FechaEncuesta, Nombre, Telefono) " +
                                         "VALUES (@FechaCaptura, @Status, @Cve_usuario, @Ip, @Cuenta, @numeroOrden, @Hub, @TipoOferta, @FechaEncuesta, @Nombre, @Telefono)";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@FechaCaptura", data["FechaCaptura"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Status", data["Status"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Cve_usuario", data["Cve_usuario"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Ip", data["Ip"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Cuenta", data["Cuenta"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@numeroOrden", data["numeroOrden"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Hub", data["Hub"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@TipoOferta", data["TipoOferta"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@FechaEncuesta", data["FechaEncuesta"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Nombre", data["Nombre"]?.ToString() ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Telefono", data["Telefono"]?.ToString() ?? (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                conn.Close();
                return Ok(new { message = "Registros procesados" });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

    }
}
