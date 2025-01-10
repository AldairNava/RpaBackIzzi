using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AjustesNotDoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AjustesNotDoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getCuentaAjustesCasosNegocioCobranza")]
        public async Task<ActionResult<IEnumerable<NotDoneAjustesModel>>> getCuentaAjustesCasosNegocioCobranza()
        {
            try
            {
                var datos = _context.AjustesBasesCasosNeogcioCobranza.FromSqlRaw("exec Sp_getCuentaAjustesCasosNegocioCobranza").ToList();
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

        [Route("ActualizaAjustesCasosNegocioCobranza")]
        [HttpPut]
        public dynamic ActualizaAjustesCasosNegocioCobranza(int id, [FromBody] NotDoneAjustesModel Cuenta)
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
        [Route("getAllAjustesCasosNegocioCobranza")]
        public async Task<ActionResult<IEnumerable<NotDoneAjustesModel>>> getAllAjustesCasosNegocioCobranza()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.AjustesBasesCasosNeogcioCobranza.FromSqlRaw($" select * from AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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
        [Route("getCasosNegocioCobranzaError")]
        public dynamic getCasosNegocioCobranzaError(string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT " +
                               "  Status AS GroupedStatus, " +
                               "  COUNT(*) AS TotalCount " +
                               "FROM AjustesBasesCasosNeogcioCobranza " +
                               $"WHERE CONVERT(date, FechaCaptura) BETWEEN '{fecha1}' AND '{fecha2}' " +
                               "  AND (Status LIKE 'Procesando' OR Status LIKE 'Error%' OR Status LIKE 'Problema%') " +
                               "GROUP BY Status";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        GroupedStatus = reader["GroupedStatus"],
                        TotalCount = reader["TotalCount"]
                    });
                }
                conn.Close();
                return objs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ActualizarStatusCasosNegocioCobranzaError")]
        public dynamic ActualizarStatusCasosNegocioCobranzaError(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update AjustesBasesCasosNeogcioCobranza set Procesando='0',Status='Registro pendiente',prioridad='0' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("ActualizarStatusajustessinvalidacionError")]
        public dynamic ActualizarStatusajustessinvalidacionError(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update AjustesSinValidacion set Procesando='0',Status='Registro pendiente',prioridad='0' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCasosSinValidacionError")]
        public dynamic getCasosSinValidacionError(string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT " +
                               "  Status AS GroupedStatus, " +
                               "  COUNT(*) AS TotalCount " +
                               "FROM AjustesSinValidacion " +
                               $"WHERE CONVERT(date, FechaCaptura) BETWEEN '{fecha1}' AND '{fecha2}' " +
                               "  AND (Status LIKE 'Procesando' OR Status LIKE 'Error%') " +
                               "GROUP BY Status";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        GroupedStatus = reader["GroupedStatus"],
                        TotalCount = reader["TotalCount"]
                    });
                }
                conn.Close();
                return objs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ActualizarStatusCasosSinValidacion")]
        public dynamic ActualizarStatusCasosSinValidacion(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update AjustesSinValidacion set Procesando='0',Status='Registro pendiente' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCasosNotDoneError")]
        public dynamic getCasosNotDoneError(string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT " +
                               "  Status AS GroupedStatus, " +
                               "  COUNT(*) AS TotalCount " +
                               "FROM EjecucionNotDone " +
                               $"WHERE CONVERT(date, FechaCaptura) BETWEEN '{fecha1}' AND '{fecha2}' " +
                               "  AND (Status LIKE 'Procesando' OR Status LIKE 'Error%') " +
                               "GROUP BY Status";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        GroupedStatus = reader["GroupedStatus"],
                        TotalCount = reader["TotalCount"]
                    });
                }
                conn.Close();
                return objs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ActualizarStatusCasoNotDone")]
        public dynamic ActualizarStatusCasoNotDone(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update EjecucionNotDone set Procesando='0',Status='Pendiente' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCasosNotDoneSinValidacionError")]
        public dynamic getCasosNotDoneSinValidacionError(string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT " +
                               "  Status AS GroupedStatus, " +
                               "  COUNT(*) AS TotalCount " +
                               "FROM CancelacionSinValidacion " +
                               $"WHERE CONVERT(date, FechaCaptura) BETWEEN '{fecha1}' AND '{fecha2}' " +
                               "  AND (Status LIKE 'Procesando' OR Status LIKE 'Error%') " +
                               "GROUP BY Status";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        GroupedStatus = reader["GroupedStatus"],
                        TotalCount = reader["TotalCount"]
                    });
                }
                conn.Close();
                return objs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getCasosDeNegocioSinValidacionError")]
        public dynamic getCasosDeNegocioSinValidacionError(string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT " +
                               "  Status AS GroupedStatus, " +
                               "  COUNT(*) AS TotalCount " +
                               "FROM CasosNegocioSinValidacion " +
                               $"WHERE CONVERT(date, FechaCaptura) BETWEEN '{fecha1}' AND '{fecha2}' " +
                               "  AND (Status LIKE 'Procesando' OR Status LIKE 'Error%') " +
                               "GROUP BY Status";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        GroupedStatus = reader["GroupedStatus"],
                        TotalCount = reader["TotalCount"]
                    });
                }
                conn.Close();
                return objs;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("ActualizarStatusCasoNotDoneSinValidacion")]
        public dynamic ActualizarStatusCasoNotDoneSinValidacion(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update CancelacionSinValidacion set Procesando='0',Status='Pendiente' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("ActualizarStatusCasosDeNegocioSinValidacion")]
        public dynamic ActualizarStatusCasosDeNegocioSinValidacion(string status, string fecha1, string fecha2)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = $"update CasosNegocioSinValidacion set Procesando='0',Status='Pendiente' where Status like '{status}%' and (convert(date,FechaCaptura) between '{fecha1}' and '{fecha2}');";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    message = "Actualizacion Correcta"
                });

                return Ok(objs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [Route("InsertarBasesNotDone")]
        [HttpPost]
        public dynamic InsertarBasesNotDone([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into EjecucionNotDone (ciudad,comentarios,creadoPor,cuenta,direccion,estadoOrden,fechaApertura,fechaSolicitada,hub,motivoCancelacion,motivoOrden,motivoReprogramacion,nombreCliente,numeroOrden,numRepro,paquete,perfilPago,plaza,referido,rpt,situacionAnticipo,subtipoCliente,subtipoOrden,tecnico,telefono,tipCliente,tipoOrden,UltimaModificacionPor,Vendedor,Cve_usuario,FechaCaptura,Status,Procesando,IP,resultadoLlamada) values ('" + data["CIUDAD"] + "','" + data["COMENTARIOS"] + "','" + data["CREADO_POR"] + "','" + data["CUENTA"] + "','" + data["DIRECCION"] + "','" + data["ESTADO_ORDEN"] + "','" + data["FECHA_APERTURA"] + "','" + data["FECHA_SOLICITADA"] + "','" + data["HUB"] + "','" + data["MOTIVO_CANCELACION"] + "','" + data["MOTIVO_ORDEN"] + "','" + data["MOTIVO_REPROGRAMACION"] + "','" + data["NOMBRE_CLIENTE"] + "','" + data["NUMERO_ORDEN"] + "','" + data["NUM_REPRO"] + "','" + data["PAQUETE"] + "','" + data["PERFIL_PAGO"] + "','" + data["PLAZA"] + "','" + data["REFERIDO"] + "','" + data["RPT"] + "','" + data["SITUACION_ANTICIPO"] + "','" + data["SUBTIPO_CLIENTE"] + "','" + data["SUBTIPO_ORDEN"] + "','" + data["TECNICO"] + "','" + data["TELEFONO"] + "','" + data["TIPO_CLIENTE"] + "','" + data["TIPO_ORDEN"] + "','" + data["ULTIMA_MOD_POR"] + "','" + data["VENDEDOR"] + "','" + data["Cve_usuario"] + "','" + data["fechaCaptura"] + "','" + data["Status"] + "','" + data["Procesando"] + "','" + data["IP"] + "','" + data["RESULTADO_LLAMADA"] +"')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return Ok(new { message = "Correo enviado" });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getCuentaEjecucionNotDone")]
        public async Task<ActionResult<IEnumerable<NotDoneModel>>> getCuentaEjecucionNotDone()
        {
            try
            {
                var datos = _context.EjecucionNotDone.FromSqlRaw("exec Sp_getCuentaEjecucionNotDone").ToList();
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

        [Route("ActualizaEjecucionNotDone")]
        [HttpPut]
        public dynamic ActualizaEjecucionNotDone(int id, [FromBody] NotDoneModel Cuenta)
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
        [Route("getAllEjecucionNotDone")]
        public async Task<ActionResult<IEnumerable<NotDoneModel>>> getAllEjecucionNotDone()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.EjecucionNotDone.FromSqlRaw($"select * from EjecucionNotDone where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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



        [Route("getStatsAjustesCasoNegocioCobranza")]
        [HttpGet]
        public dynamic getStatsAjustesCasoNegocioCobranza()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status like '%tipo cuenta%' or Status like '%Sin Saldo Vencido%' or Status like '%Excepcion%' or Status = 'Problema al Guardar Ajuste' or Status = 'Error en formato plantilla' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Aplicación correcta' or Status = 'No Aplica Ajuste reciente' or Status = 'No Aplica por estado de la cuenta' or Status = 'No Aplica promocion no detectada' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [Route("getStatsNotDone")]
        [HttpGet]
        public dynamic getStatsNotDone()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='Orden Cancelada' or status='Orden ya completa o cancelada'  THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM EjecucionNotDone where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [HttpGet]
        [Route("getAjustesTiempoAjuste")]
        public async Task<ActionResult<IEnumerable<AjustesTiempoAjusteModel>>> getAjustesTiempoAjuste()
        {
            try
            {
                var datos = _context.AjustesTiempoAjuste.FromSqlRaw("select * from AjustesTiempoAjuste order by ID desc;").ToList();
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

        [Route("ActualizaAjustesTiempoAjuste")]
        [HttpPut]
        public dynamic ActualizaAjustesTiempoAjuste(int id, [FromBody] AjustesTiempoAjusteModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    Cuenta.updatedAt = DateTime.Now;
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
        [Route("getAjustesNotDone")]
        public async Task<ActionResult<IEnumerable<AjustesNotDoneModel>>> getAjustesNotDone()
        {
            try
            {
                var datos = _context.AjustesNotDone.FromSqlRaw("select * from AjustesNotDone order by Id asc;").ToList();
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

        [Route("ActualizaAjustesNotDone")]
        [HttpPut]
        public dynamic ActualizaAjustesNotDone(int id, [FromBody] AjustesNotDoneModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    Cuenta.updatedAt = DateTime.Now;
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

        [Route("ActualizarStatus")]
        [HttpPut]
        public IActionResult ActualizarStatus(DateTime fecha)
        {
            try
            {
                // Obtén todos los registros de la tabla AjustesSinValidacion del día especificado
                var registros = _context.AjustesSinValidacion.Where(a => a.FechaCaptura == fecha.Date);

                // Verifica si hay registros para actualizar
                if (!registros.Any())
                {
                    return NotFound("No se encontraron registros para la fecha especificada.");
                }

                // Actuualiza el estado de cada registro a 'Registro pendiente'
                foreach (var registro in registros)
                {
                    registro.Status = "Registro pendiente";
                }

                // Guarda los cambios en la base de datos
                _context.SaveChanges();

                return Ok("El estado de los registros ha sido actualizado a 'Registro pendiente'.");
            }
            catch (Exception ex)
            {
                // Maneja cualquier error que pueda ocurrir
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [Route("InsertarBasesAjustesSinValidacion")]
        [HttpPost]
        public dynamic InsertarBasesAjustesSinValidacion([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into AjustesSinValidacion (cuenta,motivoAjuste,comentarioAjuste,cantidadAjustar,tipoAplicacion,Cve_usuario,IP,Procesando,Status,FechaCaptura) values ('" + data["Cuenta"] + "','" + data["Motivo ajuste"] + "','" + data["Comentario ajuste"] + "','" + data["Cantidad a ajustar"] + "','" + data["Tipo de Ajuste"] + "','" + data["Cve_usuario"] + "','" + data["IP"] + "','" + data["Procesando"] + "','" + data["Status"] + "','" + data["fechaCaptura"] + "')";
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

        [Route("InsertarBasesAjustesCasosNegocioCobranza")]
        [HttpPost]
        public dynamic InsertarBasesAjustesCasosNegocioCobranza([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into AjustesBasesCasosNeogcioCobranza (casoNegocio,categoria,cuenta,estado,fechaApertura,mediosContacto,motivoCliente,motivos,solucion,submotivo,Cve_usuario,IP,Procesando,Status,FechaCaptura,prioridad) values ('" + data["Caso de negocio"] + "','" + data["Categoria"] + "','" + data["Cuenta"] + "','" + data["Estado"] + "','" + data["Fecha de apertura"] + "','" + data["Medio de contacto"] + "','" + data["Motivo cliente"] + "','" + data["Motivos"] + "','" + data["Solución"] + "','" + data["submotivo"] + "','" + data["Cve_usuario"] + "','" + data["IP"] + "','" + data["Procesando"] + "','" + data["Status"] + "','" + data["fechaCaptura"] + "','" + 0 + "')";
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
        [Route("getCuentaAjustesSinValidacion")]
        public async Task<ActionResult<IEnumerable<AjustesSinValidacionModel>>> getCuentaAjustesSinValidacion()
        {
            try
            {
                var datos = _context.AjustesSinValidacion.FromSqlRaw("exec Sp_getCuentaAjustesSinValidacion").ToList();
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
        [Route("getAjustesSinValidacion")]
        public async Task<ActionResult<IEnumerable<AjustesSinValidacionModel>>> getAjustesSinValidacion()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.AjustesSinValidacion.FromSqlRaw($"select * from AjustesSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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

        [Route("ActualizaAjustesSinValidacion")]
        [HttpPut]
        public dynamic ActualizaAjustesSinValidacion(int id, [FromBody] AjustesSinValidacionModel Cuenta)
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
        [Route("statsBasesSinValidacion")]
        public ArrayList statsBasesSinValidacion()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            string sql = $"SELECT SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='No aplica ajuste reciente' or status='Cerrada' or status='Completado' or status='Completada' or status='Caso de Negocio YA Tipificado' or status='Aplicación correcta' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM AjustesSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
            conn.Open();
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

        [Route("InsertarBaseDatosCancelacionSinValidacion")]
        [HttpPost]
        public dynamic InsertarBaseDatosCancelacionSinValidacion([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into CancelacionSinValidacion (cuenta,numeroOrden,estado,comentariosOs,Cve_usuario,FechaCaptura,Status,Procesando) values ('" + data["Cuenta"] + "','" + data["Nº de orden"] + "','" + data["Estado"] + "','" + data["Comentarios de Os"] + "','" + data["Cve_usuario"] + "','" + data["fechaCaptura"] + "','" + data["Status"] + "','" + data["Procesando"] + "')";
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

        [Route("getStatsCancelacionSinValidacion")]
        [HttpGet]
        public dynamic getStatsCancelacionSinValidacion()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Cerrado' or Status='Completado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM CancelacionSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [HttpGet]
        [Route("getCancelacionSinValidacion")]
        public async Task<ActionResult<IEnumerable<CancelacionSinValidacionModel>>> getCancelacionSinValidacion()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.CancelacionSinValidacion.FromSqlRaw($"select * from CancelacionSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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
        [Route("getCuentaCancelacionSinValidacion")]
        public async Task<ActionResult<IEnumerable<CancelacionSinValidacionModel>>> getCuentaCancelacionSinValidacion()
        {
            try
            {
                var datos = _context.CancelacionSinValidacion.FromSqlRaw("exec Sp_getCuentaCancelacionSinValidacion").ToList();
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

        [Route("ActualizaCancelacionSinValidacion")]
        [HttpPut]
        public dynamic ActualizaCancelacionSinValidacion(int id, [FromBody] CancelacionSinValidacionModel Cuenta)
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

        [Route("InsertarBaseDatosCasosNegocioSinValidacion")]
        [HttpPost]
        public dynamic InsertarBaseDatosCasosNegocioSinValidacion([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into CasosNegocioSinValidacion (cuenta,categoria,estado,motivos,submotivos,solucion,motivoCliente,Cve_usuario,FechaCaptura,Status,Procesando,comentarios) values ('" + data["Cuenta"] + "','" + data["Categoria"] + "','" + data["Estado"] + "','" + data["Motivos"] + "','" + data["Submotivo"] + "','" + data["Solucion"] + "','" + data["Motivo Cliente"] + "','" + data["Cve_usuario"] + "','" + data["fechaCaptura"] + "','" + data["Status"] + "','" + data["Procesando"] + "','" + data["Comentarios"] + "')";
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


        [Route("getStatsCasosNegocioSinValidacion")]
        [HttpGet]
        public dynamic getStatsCasosNegocioSinValidacion()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Err%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Cerrado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM CasosNegocioSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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


        [HttpGet]
        [Route("getCasosNegocioSinValidacion")]
        public async Task<ActionResult<IEnumerable<CasosNegocioSinValidacionModel>>> getCasosNegocioSinValidacion()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.CasosNegocioSinValidacion.FromSqlRaw($"select * from CasosNegocioSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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
        [Route("getCuentaCasosNegocioSinValidacion")]
        public async Task<ActionResult<IEnumerable<CasosNegocioSinValidacionModel>>> getCuentaCasosNegocioSinValidacion()
        {
            try
            {
                var datos = _context.CasosNegocioSinValidacion.FromSqlRaw("exec Sp_getCuentaCasosNegocioSinValidacion").ToList();
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

        [Route("ActualizaCasosNegocioSinValidacion")]
        [HttpPut]
        public dynamic ActualizaCasosNegocioSinValidacion(int id, [FromBody] CasosNegocioSinValidacionModel Cuenta)
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

        [Route("InsertarBasesCreacionOrdenes")]
        [HttpPost]
        public dynamic InsertarBasesCreacionOrdenes([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into NotDoneCreacionOrdenModel (casoNegocio,categoria,cuenta,estado,fechaApertura,mediosContacto,motivoCliente,motivos,solucion,submotivo,Cve_usuario,IP,Procesando,Status,FechaCaptura) values ('" + data["Caso de negocio"] + "','" + data["Categoria"] + "','" + data["Cuenta"] + "','" + data["Estado"] + "','" + data["Fecha de apertura"] + "','" + data["Medio de contacto"] + "','" + data["Motivo Cliente"] + "','" + data["Motivos"] + "','" + data["Solución"] + "','" + data["Submotivo"] + "','" + data["Cve_usuario"] + "','" + data["IP"] + "','" + data["Procesando"] + "','" + data["Status"] + "','" + data["fechaCaptura"] + "')";
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
        [Route("getCuentaCreacionOrdenes")]
        public async Task<ActionResult<IEnumerable<NotDoneCreacionOrdenModel>>> getCuentaCreacionOrdenes()
        {
            try
            {
                var datos = _context.NotDoneCreacionOrdenModel.FromSqlRaw("exec Sp_getCuentaCreacionOrden").ToList();
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

        [Route("ActualizaCreacionOrden")]
        [HttpPut]
        public dynamic ActualizaCreacionOrden(int id, [FromBody] NotDoneCreacionOrdenModel Cuenta)
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


        [Route("getStatsCrearCuenta")]
        [HttpGet]
        public dynamic getStatsCrearCuenta()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status like '%tipo cuenta%' or Status like '%Sin Saldo Vencido%' or Status like '%Excepcion%' or Status like 'No Aplica%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='CN Cerrado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM NotDoneCreacionOrdenModel where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [HttpGet]
        [Route("getAllCrearCuenta")]
        public async Task<ActionResult<IEnumerable<NotDoneCreacionOrdenModel>>> getAllCrearCuenta()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.NotDoneCreacionOrdenModel.FromSqlRaw($" select * from NotDoneCreacionOrdenModel where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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

        //[Route("updateBasesCasoNegocioCobranza")]
        //[HttpPost]
        //public dynamic updateBasesCasoNegocioCobranza([FromBody] JsonArray Info)
        //{
        //    SqlConnection conn = new SqlConnection();
        //    conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
        //    try
        //    {
        //        conn.Open();
        //        foreach (var data in Info)
        //        {
        //            string sql = "update AjustesBasesCasosNeogcioCobranza set Status= '"+data["Status"] +"' where id='" + data["id"] + "';";
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.ExecuteNonQuery();
        //        }
        //        conn.Close();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        conn.Close();
        //        return BadRequest(ex.Message);
        //    }

        //}

        [Route("InsertarBasesOrdenTrouble")]
        [HttpPost]
        public dynamic InsertarBasesOrdenTrouble([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into OrdenTroubleCall (Cuenta,Tipo,Motivo,Comentarios,Cve_usuario,Procesando,Status,FechaCaptura) " +
                                 "values ('" + data["Cuenta"] + "','" + data["Tipo"] + "','" + data["Motivo"] + "','" + data["Comentarios"] + "','" + data["Cve_usuario"] + "','" + data["Procesando"] + "','" + data["Status"] + "','" + data["FechaCaptura"] + "')";
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

        //[Route("InsertarBasesOrdenTrouble")]
        //[HttpPost]
        //public dynamic InsertarBasesOrdenTrouble([FromBody] JsonArray Info)
        //{
        //    SqlConnection conn = new SqlConnection();
        //    conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
        //    try
        //    {
        //        conn.Open();
        //        foreach (var data in Info)
        //        {
        //            string sql = "insert into OrdenTroubleCall (Cuenta,Tipo,Motivo,Comentarios,Cve_usuario,Procesando,Status,FechaCaptura,NumeroOrden,FechaCompletado,Ip,NumeroCN) " +
        //     "values ('" + data["Cuenta"] + "','" + data["Tipo"] + "','" + data["Motivo"] + "','" + data["Comentarios"] + "','" + data["Usuario"] + "','" + data["Procesando"] + "','" + data["Status"] + "','" + data["Fecha Capturado"] + "','" + data["Numero de Orden"] + "','" + data["Fecha Completado"] + "','" + data["Bot"] + "','" + data["Numero de CN"] + "')";
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.ExecuteNonQuery();
        //        }
        //        conn.Close();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        conn.Close();
        //        return BadRequest(ex.Message);
        //    }

        //}

        [Route("ActualizaOrdenTroubleCall")]
        [HttpPut]
        public dynamic ActualizaOrdenTroubleCall(int id, [FromBody] ordenTroubleCall Cuenta)
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
        [Route("getCuentaOrdenTroubleCall")]
        public async Task<ActionResult<IEnumerable<ordenTroubleCall>>> getCuentaOrdenTroubleCall()
        {
            try
            {
                var datos = _context.OrdenTroubleCall.FromSqlRaw("exec Sp_getCuentaOrdenTroubleCall").ToList();
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

        [Route("getStatsOrdeneCallTrouble")]
        [HttpGet]
        public dynamic getStatsOrdeneCallTrouble()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status like '%tipo cuenta%' or Status like '%Sin Saldo Vencido%' or Status like '%Excepcion%' or Status = 'Problema al Guardar Ajuste' or Status = 'Error en formato plantilla' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Aplicación correcta' or Status = 'No Aplica Ajuste reciente' or Status = 'No Aplica por estado de la cuenta' or Status = 'No Aplica promocion no detectada' or Status = 'Confirmado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM OrdenTroubleCall where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [HttpGet]
        [Route("getDiaOrdenCallTrouble")]
        public async Task<ActionResult<IEnumerable<ordenTroubleCall>>> getDiaOrdenCallTrouble()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.OrdenTroubleCall.FromSqlRaw($" select * from OrdenTroubleCall where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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


    }
}
