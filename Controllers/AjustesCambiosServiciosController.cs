using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AjustesCambiosServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        public AjustesCambiosServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("InsertarBaseDatosAjustesExcel")]
        [HttpPost]
        public dynamic InsertarBaseDatosAjustesExcel([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "IF NOT EXISTS (SELECT * FROM AjustesCambioServicios where casoNegocio='" + data["Caso de negocio"] + "') BEGIN insert into AjustesCambioServicios (cuenta,casoNegocio,Cve_usuario,motivo,motivoCliente,estado,Status,Procesando,FechaCreado,FechaCarga,FechaVencimiento) values (" +
                      "'" + data["Nº de cuenta"] + "'," + data["Caso de negocio"] + ",'" + data["Cve_usuario"] + "','" + data["Motivo"] + "','" + data["Motivo Cliente"] + "','" + data["Estado"] + "','" + data["Status"] + "','" + data["Procesando"] + "','" + data["Creado_1"] + "','" + data["Fecha_Carga"] + "','" + data["Vencimiento"] + "') END";
                    //string sql = "string sql = \"IF NOT EXISTS (SELECT * FROM AjustesCambioServicios WHERE casoNegocio='\" + data[\"Nº de caso de negocio\"] + \"') BEGIN INSERT INTO AjustesCambioServicios (cuenta, casoNegocio, Cve_usuario, comentarios, comentariosCN, estado, [Status], Procesando, FechaCreado, FechaCarga, FechaAsignacion, FechaVencimiento) VALUES (\" +\r\n                      \"'\" + data[\"Cliente\"] + \"','\" + data[\"Nº de caso de negocio\"] + \"','\" + data[\"Cve_usuario\"] + \"','\" + data[\"Comentarios\"] + \"','\" + data[\"Comentarios CN\"] + \"','\" + data[\"Estado\"] + \"','\" + data[\"Status\"] + \"','\" + data[\"Procesando\"] + \"','\" + data[\"Creado_1\"] + \"','\" + data[\"Fecha_Carga\"] + \"','\" + data[\"Fecha de asignación\"] + \"','\" + data[\"Vencimiento\"] + \"') END\";";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("getCuentaAjustesCambioServicio")]
        public async Task<ActionResult<IEnumerable<AjustesCambiosServiciosModel>>> getCuentaAjustesCambioServicio()
        {
            try
            {
                var datos = _context.AjustesCambioServicios.FromSqlRaw("exec Sp_getCuentaAjustesCambioServicio").ToList();
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

        [Route("ActualizaAjustesCambioServicio")]
        [HttpPut]
        public dynamic ActualizaAjustesCambioServicio(int id, [FromBody] AjustesCambiosServiciosModel Cuenta)
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
        [Route("getAjustesCambioServicioInfo")]
        public async Task<ActionResult<IEnumerable<AjustesCambiosServiciosModel>>> getAjustesCambioServicioInfo()
        {
            try
            {
                var datos = _context.AjustesCambioServicios.FromSqlRaw("select * from AjustesCambioServicios order by FechaCarga desc;").ToList();
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

        [Route("getStatsAjustesCambioServicio")]
        [HttpGet]
        public dynamic getStatsAjustesCambioServicio()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='Cerrada' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM AjustesCambioServicios where CONVERT(date,FechaCarga) between '{Date}' and '{Date}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList objs = new ArrayList();
            while (reader.Read())
            {
                objs.Add(new
                {
                    Pendientes = reader["Pendientes"],
                    Error = reader["Error"],
                    Completado = reader["Completado"],
                    Procesando = reader["Procesando"],
                    Total = reader["Total"],
                });
            }
            conn.Close();
            return objs;

        }


        [Route("InsertarBaseDatosMigracionesLineales")]
        [HttpPost]
        public dynamic InsertarBaseDatosMigracionesLineales([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "IF NOT EXISTS (SELECT * FROM MigracionesLinealeS where cuenta='" + data["CUENTA"] + "') BEGIN insert into MigracionesLinealeS (beneficioOtorgado,cn,cuenta,canal,fechaBenOtorgada,fechaEnvioCiber,fechaRecibidoBO,fechaCarga,nombreCliente,os,paqueteOrigen,estatus,subMotivo,usuario,Cve_usuario,Status,Procesando,IP,FechaCaptura) values ('" + data["BENEFICIO OTORGADO"] + "','" + data["CN"] + "','" + data["CUENTA"] + "','" + data["Canal"] + "','" + data["FECHA BEN OTORGADO"] + "','" + data["FECHA ENVIO A CIBER"] + "','" + data["FECHA RECIBIDO BO"] + "','" + data["Fecha Carga"] + "','" + data["NOMBRE DEL CLIENTE"] + "','" + data["OS"] + "','" + data["PAQUETE ORIGEN"] + "','" + data["Status"] + "','" + data["Sub motivo"] + "','" + data["Usuario"] + "','" + data["Cve_usuario"] + "','" + data["Status1"] + "','" + data["Procesando"] + "','" + data["IP"] + "','" + data["FechaCaptura"] + "') END";
                 
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCuentaMigracionesLineales")]
        public async Task<ActionResult<IEnumerable<MigracionesLinealesModel>>> getCuentaMigracionesLineales()
        {
            try
            {
                var datos = _context.MigracionesLinealeS.FromSqlRaw("exec Sp_getCuentaMigracionesLinealeS").ToList();
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

        [Route("ActualizaMigracionesLineales")]
        [HttpPut]
        public dynamic ActualizaMigracionesLineales(int id, [FromBody] MigracionesLinealesModel Cuenta)
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
        [Route("getMigracionesLineales")]
        public async Task<ActionResult<IEnumerable<MigracionesLinealesModel>>> getMigracionesLineales()
        {
            try
            {
                var datos = _context.MigracionesLinealeS.FromSqlRaw("select * from MigracionesLineales order by FechaCaptura desc;").ToList();
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



        [Route("getStatsMigracionesLineales")]
        [HttpGet]
        public dynamic getStatsMigracionesLineales()
        {
            DateTime Date = DateTime.Today;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='Cerrada' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM MigracionesLinealeS where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ArrayList objs = new ArrayList();
            while (reader.Read())
            {
                objs.Add(new
                {
                    Pendientes = reader["Pendientes"],
                    Error = reader["Error"],
                    Completado = reader["Completado"],
                    Procesando = reader["Procesando"],
                    Total = reader["Total"],
                });
            }
            conn.Close();
            return objs;

        }


        [Route("InsertarBasesRetencion0")]
        [HttpPost]
        public dynamic InsertarBasesRetencion0([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var item in Info)
                {
                    var data = (JsonObject)item;

                    string sql = "INSERT INTO Retencion (FechaCaptura, Status, Cve_usuario, Ip, Cuenta, CasoNegocio, Proceso, Equipo) " +
                                 "VALUES (@FechaCaptura,@Status, @Cve_usuario, @Ip,@Cuenta, @CasoNegocio, @Proceso, @Equipo)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@FechaCaptura", data["FechaCaptura"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", data["Status"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cve_usuario", data["Cve_usuario"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ip", data["Ip"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cuenta", data["Cuenta"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CasoNegocio", data["CasoNegocio"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Proceso", data["Proceso"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Equipo", data["EQUIPO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return Ok(new { message = "registros insertados" });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

        [Route("getStatusBaseRetencion")]
        [HttpGet]
        public async Task<IActionResult> getBaseRetencion()
        {
            var date = DateTime.Now.Date.ToString("yyyy-MM-dd");

            string connectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            string sql = @"
        SELECT 
            SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,
            SUM(CASE 
                WHEN Status LIKE '%Error%'  or status like '%Inconsistencia%'
                THEN 1 ELSE 0 END) AS Error,
            SUM(CASE 
                WHEN Status = 'Cerrado'  
                THEN 1 ELSE 0 END) AS Completado,
            SUM(CASE 
                WHEN Status like '%No Aplica%'  
                THEN 1 ELSE 0 END) AS noaplica,
            SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,
            COUNT(*) AS Total
        FROM Retencion 
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
                                    NoAplica = reader.GetInt32(reader.GetOrdinal("noaplica")),
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

        [Route("ActualizaRetencion")]
        [HttpPut]
        public dynamic ActualizaRetencion(int id, [FromBody] Retencion Cuenta)
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
        [Route("getAllRetencion")]
        public async Task<ActionResult<IEnumerable<Retencion>>> getAllRetencion()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                //var Date = DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd");
                var datos = _context.Retencion.FromSqlRaw($" select * from Retencion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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
        [Route("getCuentaRetencion")]
        public async Task<ActionResult<IEnumerable<Retencion>>> getCuentaRetencion()
        {
            try
            {
                var datos = _context.Retencion.FromSqlRaw("exec Sp_getCuentaRetencion").ToList();
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
        [Route("getCatProcesosRetencion")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsRetencionaModel>>> getCatProcesosRetencion()
        {
            try
            {
                var datos = await _context.cat_procesosRetencion.Select(proceso => new catalogoProcesosBotsRetencionaModel
                {
                    Id = proceso.Id,
                    Name_usuario = proceso.Name_usuario,
                    Status = proceso.Status,
                    Name_process = proceso.Name_process
                }).ToListAsync();

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
        [Route("updateProcessStatusBotsRetencion")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsRetencionaModel>>> updateProcessStatusBotsRetencion(string ip, string estado)
        {
            try
            {
                var query = from bot in _context.BotsProcessRetencion
                            join proceso in _context.cat_procesosRetencion on bot.ProcesoBotId equals proceso.Id
                            where bot.ip == ip
                            select bot;

                var datos = await query.FirstOrDefaultAsync();

                if (datos != null && datos.ProcesoBotId != null)
                {
                    datos.estado = estado;
                    await _context.SaveChangesAsync();
                    return Ok("Estado Actualizado");

                }
                else
                {
                    var d = new List<string>()
                    {
                        "Bot no encontrado con la IP"
                    };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("ActualizarBotProcessRetencion")]
        [HttpPut]
        public dynamic ActualizarBotProcessRetencion(int id, [FromBody] BotsModelRetencion Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcessRetencion.FirstOrDefault(b => b.Id == id);

                    if (bot != null)
                    {
                        bot.ProcesoBotId = Cuenta.ProcesoBotId;
                        _context.SaveChanges();

                        return Ok(Cuenta);
                    }
                    return NotFound();
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
        [Route("getBotsRetencion")]
        public async Task<ActionResult<IEnumerable<BotsModelRetencion>>> getBotsRetencion()
        {
            try
            {
                var bots = await _context.BotsProcessRetencion.ToListAsync();
                if (bots.Count > 0)
                {
                    return Ok(bots);
                }
                else
                {
                    return Ok(new List<string> { "SIN INFO" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getValidationProcesosRetencion")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsRetencionaModel>>> getValidationProcesosLimpieza()
        {
            try
            {
                var query = from b in _context.BotsProcessLimpieza
                            join c in _context.cat_procesosLimpieza on b.ProcesoBotId equals c.Id
                            group new { c, b } by new { c.Name_process, b.ProcesoBotId } into grouped
                            select new
                            {
                                ProcesoName = grouped.Key.Name_process,
                                ProcesoBotId = grouped.Key.ProcesoBotId,
                                Num = grouped.Count()
                            };
                if (query.Count() > 0)
                {
                    return Ok(query);

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
        [Route("getBotsEstadoRetencion")]
        public async Task<ActionResult<IEnumerable<BotsModelRetencion>>> getBotsEstadoRetencion()
        {
            try
            {
                var query = from bot in _context.BotsProcessRetencion
                            orderby bot.ip ascending
                            select new
                            {
                                BotId = bot.Id,
                                BotIp = bot.ip,
                                BotEstado = bot.estado,
                            };
                var datos = query.ToList();
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
        [Route("cambiarProcesosIzziRetencion")]
        public async Task<IActionResult> cambiarProcesosIzziRetencion(string ip, string proceso, string status)
        {
            try
            {
                string apiUrl = $"http://{ip}:3000/update_env?proceso={proceso}&status={status}";


                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return Ok(responseBody);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("EliminarBotRetecnion")]
        [HttpDelete]
        public dynamic EliminarBotRetecnion(int id, [FromBody] BotsModelRetencion Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    _context.Remove(Cuenta);
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
        [Route("getBotById/{id}")]
        public async Task<ActionResult<BotsModelRetencion>> getBotById(int id)
        {
            try
            {
                var query = from bot in _context.BotsProcessRetencion
                            join proceso in _context.cat_procesosRetencion on bot.ProcesoBotId equals proceso.Id
                            where bot.Id == id
                            select new
                            {
                                BotId = bot.Id,
                                BotComentarios = bot.comentarios,
                                BotHostName = bot.hostName,
                                BotFechaActualizacion = bot.fechaActualizacion,
                                BotIp = bot.ip,
                                BotProcesoId = bot.ProcesoBotId,
                                ProcesoId = proceso.Id,
                                ProcesoName = proceso.Name_process,
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                                ProcesoFechaActualizacion = proceso.update_At,
                                ProcesoStatus = proceso.Status
                            };

                var bot1 = await query.FirstOrDefaultAsync();

                if (bot1 != null)
                {
                    return Ok(bot1);
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

        [Route("ActualizarBot")]
        [HttpPut]
        public dynamic ActualizarBot(int id, [FromBody] BotsModelRetencion Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcessRetencion.FirstOrDefault(b => b.Id == id);
                    var proceso = _context.cat_procesosRetencion.FirstOrDefault(p => p.Id == Cuenta.ProcesoBotId);

                    if (bot != null && proceso != null)
                    {
                        bot.fechaActualizacion = DateTime.Now;
                        bot.comentarios = Cuenta.comentarios;
                        bot.hostName = Cuenta.hostName;
                        bot.ProcesoBotId = Cuenta.ProcesoBotId;
                        bot.ip = Cuenta.ip;

                        if (proceso.password != Cuenta.ProcesoBot.password)
                        {
                            proceso.update_At = DateTime.Now;
                            proceso.password = Cuenta.ProcesoBot.password;
                        }
                        proceso.usuario = Cuenta.ProcesoBot.usuario;

                        _context.SaveChanges();

                    }
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
        [Route("getProcessOne")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsRetencionaModel>>> getProcessOne(int id)
        {
            try
            {
                var query = from proceso in _context.cat_procesosRetencion
                            where proceso.Id == id
                            select new
                            {
                                ProcesoName = proceso.Name_process,
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                            };

                var datos = await query.FirstOrDefaultAsync();
                if (datos != null)
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


    }
}
