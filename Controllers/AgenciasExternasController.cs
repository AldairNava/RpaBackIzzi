using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgenciasExternasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public AgenciasExternasController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        // Cadena de conexión y sentencia SQL a nivel de clase
        private const string ConnectionString =
            "Server=tcp:rpawinserver.database.windows.net,1433;" +
            "Initial Catalog=WinDBRPA;Persist Security Info=False;" +
            "User ID=RpaWinDB;Password=Ruka0763feTrfg;" +
            "MultipleActiveResultSets=False;Encrypt=True;" +
            "TrustServerCertificate=False;Connection Timeout=120;";

        private const string InsertSql = @"
            INSERT INTO CreacionCNs
                (FechaCaptura, Status, Cve_usuario, Ip, Cuenta, FechaSubida,
                 Categoria, Mootivo, SubMotivo, Solucion, SaldoIncobrable,
                 Promocion, Ajuste, FechaGestion, Tipo)
            VALUES
                (@FechaCaptura, @Status, @Cve_usuario, @Ip, @Cuenta, @FechaSubida,
                 @Categoria, @Mootivo, @SubMotivo, @Solucion, @SaldoIncobrable,
                 @Promocion, @Ajuste, @FechaGestion, @Tipo);";

        // Método auxiliar: obtiene un string o el texto bruto de un número
        private static string? GetStringOrNumber(JsonObject obj, string key)
        {
            if (!obj.TryGetPropertyValue(key, out var node) || node is null)
                return null;

            var elem = node.GetValue<JsonElement>();
            return elem.ValueKind switch
            {
                JsonValueKind.String => elem.GetString(),
                JsonValueKind.Number => elem.GetRawText(),
                _ => elem.ToString()
            };
        }

        [HttpPost("InsertarBasesCrearCNs")]
        public async Task<IActionResult> InsertarBasesCrearCNs([FromBody] JsonArray info)
        {
            await using var conn = new SqlConnection(ConnectionString);

            try
            {
                await conn.OpenAsync();

                foreach (var itemNode in info)
                {
                    if (itemNode is not JsonObject obj)
                        continue;

                    var ahora = DateTime.Now;

                    // Campos extraídos como string
                    string? status = GetStringOrNumber(obj, "Status");
                    string? cveUsuario = GetStringOrNumber(obj, "Cve_usuario");
                    string? ip = GetStringOrNumber(obj, "Ip");
                    string? cuenta = GetStringOrNumber(obj, "Cuenta");
                    DateTime? fechaSubida = DateTime.TryParse(GetStringOrNumber(obj, "FechaSubida"), out var fs) ? fs : null;
                    string? categoria = GetStringOrNumber(obj, "Categoria");
                    string? mootivo = GetStringOrNumber(obj, "Mootivo");
                    string? subMotivo = GetStringOrNumber(obj, "SubMotivo");
                    string? solucion = GetStringOrNumber(obj, "Solucion");

                    // Leemos el valor bruto de SaldoIncobrable y Ajuste
                    string? rawSaldo = GetStringOrNumber(obj, "SaldoIncobrable");
                    string? rawAjuste = GetStringOrNumber(obj, "Ajuste");
                    // Promoción sigue siendo puro string
                    string? promocion = GetStringOrNumber(obj, "Promocion");

                    // Formateamos a dos decimales (o null si no parsea)
                    string? saldoIncobrable = null;
                    if (!string.IsNullOrWhiteSpace(rawSaldo)
                        && decimal.TryParse(rawSaldo, NumberStyles.Any, CultureInfo.InvariantCulture, out var decSaldo))
                    {
                        saldoIncobrable = decSaldo.ToString("F2", CultureInfo.InvariantCulture);
                    }

                    string? ajuste = null;
                    if (!string.IsNullOrWhiteSpace(rawAjuste)
                        && decimal.TryParse(rawAjuste, NumberStyles.Any, CultureInfo.InvariantCulture, out var decAjuste))
                    {
                        ajuste = decAjuste.ToString("F2", CultureInfo.InvariantCulture);
                    }

                    DateTime? fechaGestion = DateTime.TryParse(GetStringOrNumber(obj, "FechaGestion"), out var fg) ? fg : null;
                    string? tipo = GetStringOrNumber(obj, "Tipo");

                    await using var cmd = new SqlCommand(InsertSql, conn);
                    cmd.Parameters.AddWithValue("@FechaCaptura", ahora);
                    cmd.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cve_usuario", (object?)cveUsuario ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ip", (object?)ip ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cuenta", (object?)cuenta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaSubida", (object?)fechaSubida ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Categoria", (object?)categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mootivo", (object?)mootivo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SubMotivo", (object?)subMotivo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Solucion", (object?)solucion ?? DBNull.Value);

                    // Ahora con dos decimales
                    cmd.Parameters.Add("@SaldoIncobrable", SqlDbType.NVarChar, 50)
                       .Value = (object?)saldoIncobrable ?? DBNull.Value;
                    cmd.Parameters.Add("@Promocion", SqlDbType.NVarChar, 50)
                       .Value = (object?)promocion ?? DBNull.Value;
                    cmd.Parameters.Add("@Ajuste", SqlDbType.NVarChar, 50)
                       .Value = (object?)ajuste ?? DBNull.Value;

                    cmd.Parameters.AddWithValue("@FechaGestion", (object?)fechaGestion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tipo", (object?)tipo ?? DBNull.Value);

                    await cmd.ExecuteNonQueryAsync();
                }

                await conn.CloseAsync();
                return Ok(new { message = "Registros insertados correctamente", count = info.Count });
            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    await conn.CloseAsync();
                return BadRequest(new { error = ex.Message });
            }
        }



        [Route("getStatsCreacionCNs")]
        [HttpGet]
        public async Task<IActionResult> getStatsCreacionCNs()
        {
            var date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            string sql = @"
                SELECT 
                    SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,
                    SUM(CASE WHEN Status LIKE '%Error%' THEN 1 ELSE 0 END)      AS Error,
                    SUM(CASE WHEN Status = 'Cerrado' THEN 1 ELSE 0 END)        AS Completado,
                    SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END)     AS Procesando,
                    COUNT(*)                                                  AS Total
                FROM CreacionCNs 
                WHERE CONVERT(date, FechaCaptura) BETWEEN @StartDate AND @EndDate";

            var result = new List<object>();

            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StartDate", date);
                cmd.Parameters.AddWithValue("@EndDate", date);

                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
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
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("ActualizaCreacionCNs")]
        public IActionResult ActualizaCreacionCNs(int id, [FromBody] CreacionCNs Cuenta)
        {
            try
            {
                if (id != Cuenta.Id)
                    return NotFound();

                Cuenta.FechaCompletado = DateTime.Now;
                _context.Update(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllCreacionCNs")]
        public ActionResult<IEnumerable<CreacionCNs>> getAllCreacionCNs()
        {
            try
            {
                var date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.CreacionCNs
                    .FromSqlRaw($"SELECT * FROM CreacionCNs WHERE CONVERT(date,FechaCaptura) BETWEEN '{date}' AND '{date}' ORDER BY FechaCaptura DESC")
                    .ToList();

                if (datos.Count > 0)
                    return Ok(datos);

                return Ok(new List<string> { "SIN INFO" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCuentaCreacionCNs")]
        public ActionResult<IEnumerable<CreacionCNs>> getCuentaCreacionCNs()
        {
            try
            {
                var datos = _context.CreacionCNs
                    .FromSqlRaw("EXEC Sp_getCuentaCreacionCNs")
                    .ToList();

                if (datos.Count > 0)
                    return Ok(datos);

                return Ok(new List<string> { "SIN INFO" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertDepuracion")]
        public ActionResult insertDepuracion([FromBody] AgenteDepuracion nuevo)
        {
            try
            {
                if (nuevo == null)
                    return BadRequest("Datos inválidos.");

                _context.AgenteDepuracion.Add(nuevo);
                _context.SaveChanges();

                return Ok("Registro insertado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al insertar: {ex.Message}");
            }
        }



        [HttpGet("getDepuracion")]
        public ActionResult<IEnumerable<AgenteDepuracion>> getDepuracion()
        {
            try
            {
                var datos = _context.AgenteDepuracion
                    .Where(a => a.status == "Marcando")
                    .ToList();

                if (!datos.Any())
                    return Ok(new List<string> { "SIN INFO" });
                datos.ForEach(a => a.status = "llamada");
                _context.SaveChanges();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getCuentaAgenteDepuracion")]
        public ActionResult<IEnumerable<AgenteDepuracion>> getCuentaAgenteDepuracion()
        {
            try
            {
                var datos = _context.AgenteDepuracion
                    .FromSqlRaw("EXEC Sp_getCuentaDepuracionAI")
                    .ToList();

                if (datos.Count > 0)
                    return Ok(datos);

                return Ok(new List<string> { "SIN INFO" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ActualizaAgentedepuracion")]
        public IActionResult ActualizaAgentedepuracion(int id, [FromBody] AgenteDepuracion Cuenta)
        {
            try
            {
                if (id != Cuenta.Id)
                    return NotFound();

                Cuenta.FechaCompletado = DateTime.Now;
                _context.Update(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
