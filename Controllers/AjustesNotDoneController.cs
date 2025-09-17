using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;
using System.Text.Json.Nodes;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

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

        private static readonly object _lock = new object();
        private static DateTime _lastRequestUtc = DateTime.MinValue;

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
                             "  AND Status != 'Error al crear cn' " +
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
                var registros = _context.AjustesSinValidacion.Where(a => a.FechaCaptura == fecha.Date);

                if (!registros.Any())
                {
                    return NotFound("No se encontraron registros para la fecha especificada.");
                }

                foreach (var registro in registros)
                {
                    registro.Status = "Registro pendiente";
                }

                _context.SaveChanges();

                return Ok("El estado de los registros ha sido actualizado a 'Registro pendiente'.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [Route("InsertarBasesAjustesSinValidacion")]
        [HttpPost]
        public IActionResult InsertarBasesAjustesSinValidacion([FromBody] JsonArray info)
        {
            var conn = new SqlConnection
            {
                ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;"
            };

            using var cmd = new SqlCommand("dbo.sp_InsertarAjustesSinValidacion", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            string json = info.ToJsonString();
            cmd.Parameters.AddWithValue("@json", json);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Ok(new { message = "Registros procesados en bloque" });
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
        public async Task<ActionResult> GetCuentaAjustesSinValidacion()
        {

            try
            {
                var datos = await _context
                    .AjustesSinValidacion
                    .FromSqlRaw("EXEC Sp_getCuentaAjustesSinValidacion")
                    .ToListAsync();

                if (datos.Any())
                    return Ok(datos);
                else
                    return Ok(new List<string> { "SIN INFO" });
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
            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Cerrado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM CasosNegocioSinValidacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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


        [HttpPost]
        [Route("reprocesarIndividual")]
        public IActionResult ReprocesarIndividual([FromBody] JsonObject info)
        {
            try
            {
                // 1) Validar payload
                if (info == null
                    || info["id"] == null
                    || info["proceso"] == null
                    || info["status"] == null
                    || info["usuario"] == null)
                {
                    return BadRequest(new { message = "Petición inválida. Debe incluir id, proceso, status y usuario." });
                }

                int id = info["id"]!.GetValue<int>();
                string proceso = info["proceso"]!.GetValue<string>();
                string status = info["status"]!.GetValue<string>();
                string usuario = info["usuario"]!.GetValue<string>();

                // 2) Definir el mapa de procesos
                var configProcesos = new Dictionary<string, (string tabla, bool incluyeProcesando)>(StringComparer.OrdinalIgnoreCase)
                {
                    // Procesos que usan la tabla AjustesSinValidacion y requieren Procesando = 0
                    ["AjustesSinValidacion"] = ("AjustesSinValidacion", true),
                    ["DepuracionBasesCanceladasOS"] = ("DepuracionBasesCanceladasOS", true),
                    ["AjustesBasesCasosNeogcioCobranza"] = ("AjustesBasesCasosNeogcioCobranza", true),
                    ["CasosNegocioSinValidacion"] = ("CasosNegocioSinValidacion", true),
                    ["EjecucionNotDone"] = ("EjecucionNotDone", true),
                    ["DepuracionBasesCanceladasOSExt"] = ("DepuracionBasesCanceladasOSExt", true),
                    ["NotDoneCreacionOrdenModel"] = ("NotDoneCreacionOrdenModel", true),
                    ["OrdenTroubleCall"] = ("OrdenTroubleCall", true),

                    ["okCliente2"] = ("okCliente2", false),
                    ["Retencion0"] = ("Retencion0", false),
                    ["CreacionCNs"] = ("CreacionCNs", false),

                };

                // 3) Buscar la configuración del proceso
                if (!configProcesos.TryGetValue(proceso, out var cfg))
                {
                    return BadRequest(new { message = $"Proceso '{proceso}' no reconocido." });
                }

                // 4) Abrir conexión
                using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
                conn.Open();

                // 5) Construir UPDATE dinámico
                string sqlUpdate;
                if (cfg.incluyeProcesando)
                {
                    sqlUpdate = $@"
                UPDATE {cfg.tabla}
                SET
                  status           = @status,
                  Procesando       = 0
                WHERE Id = @id;
            ";
                }
                else
                {
                    sqlUpdate = $@"
                UPDATE {cfg.tabla}
                SET
                  Status           = @status
                WHERE Id = @id;
            ";
                }

                using (var cmdUpd = new SqlCommand(sqlUpdate, conn))
                {
                    cmdUpd.Parameters.AddWithValue("@id", id);
                    cmdUpd.Parameters.AddWithValue("@status", status);
                    cmdUpd.Parameters.AddWithValue("@usuario", usuario);
                    cmdUpd.ExecuteNonQuery();
                }

                string sqlInsert = @"
            INSERT INTO LogReprocesos (
                id_Reprocesado,
                usuario,
                FechaReproceso,
                Proceso
            ) VALUES (
                @id,
                @usuario,
                DATEADD(hour, -6, GETDATE()),
                @proceso
            );
        ";
                using (var cmdIns = new SqlCommand(sqlInsert, conn))
                {
                    cmdIns.Parameters.AddWithValue("@id", id);
                    cmdIns.Parameters.AddWithValue("@usuario", usuario);
                    cmdIns.Parameters.AddWithValue("@proceso", proceso);
                    cmdIns.Parameters.AddWithValue("@status", status);
                    cmdIns.ExecuteNonQuery();
                }

                return Ok(new { message = "Reproceso registrado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al reprocesar.", detail = ex.Message });
            }
        }


        [HttpPost]
        [Route("reprocesarMasivo")]
        public IActionResult ReprocesarMasivo([FromBody] JsonObject info)
        {
            try
            {
                if (info == null
                    || info["registros"] == null
                    || info["usuario"] == null)
                {
                    return BadRequest(new { message = "Petición inválida. Debe incluir 'registros' y 'usuario'." });
                }

                string usuario = info["usuario"]!.GetValue<string>();
                var registros = info["registros"]!.AsArray();
                if (registros.Count == 0)
                {
                    return BadRequest(new { message = "No se proporcionaron registros para reprocesar." });
                }

                var configProcesos = new Dictionary<string, (string tabla, bool incluyeProcesando)>(StringComparer.OrdinalIgnoreCase)
                {
                    ["AjustesSinValidacion"] = ("AjustesSinValidacion", true),
                    ["DepuracionBasesCanceladasOS"] = ("DepuracionBasesCanceladasOS", true),
                    ["AjustesBasesCasosNeogcioCobranza"] = ("AjustesBasesCasosNeogcioCobranza", true),
                    ["CasosNegocioSinValidacion"] = ("CasosNegocioSinValidacion", true),
                    ["EjecucionNotDone"] = ("EjecucionNotDone", true),
                    ["DepuracionBasesCanceladasOSExt"] = ("DepuracionBasesCanceladasOSExt", true),
                    ["NotDoneCreacionOrdenModel"] = ("NotDoneCreacionOrdenModel", true),
                    ["OrdenTroubleCall"] = ("OrdenTroubleCall", true),

                    ["okCliente2"] = ("okCliente2", false),
                    ["Retencion0"] = ("Retencion0", false),
                    ["CreacionCNs"] = ("CreacionCNs", false),

                };

                using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
                conn.Open();

                int procesados = 0;
                foreach (var node in registros)
                {
                    var obj = node?.AsObject();
                    if (obj == null
                        || obj["id"] == null
                        || obj["proceso"] == null
                        || obj["status"] == null)
                    {
                        continue;
                    }

                    int id = obj["id"]!.GetValue<int>();
                    string proceso = obj["proceso"]!.GetValue<string>();
                    string status = obj["status"]!.GetValue<string>();

                    if (!configProcesos.TryGetValue(proceso, out var cfg))
                    {
                        continue;
                    }

                    string sqlUpdate;
                    if (cfg.incluyeProcesando)
                    {
                        sqlUpdate = $@"
                    UPDATE {cfg.tabla}
                    SET
                      Status           = @status,
                      Procesando       = 0
                    WHERE Id = @id;
                ";
                    }
                    else
                    {
                        sqlUpdate = $@"
                    UPDATE {cfg.tabla}
                    SET
                      Status           = @status
                    WHERE Id = @id;
                ";
                    }

                    using (var cmdUpd = new SqlCommand(sqlUpdate, conn))
                    {
                        cmdUpd.Parameters.AddWithValue("@id", id);
                        cmdUpd.Parameters.AddWithValue("@status", status);
                        cmdUpd.Parameters.AddWithValue("@usuario", usuario);
                        cmdUpd.ExecuteNonQuery();
                    }

                    var sqlInsert = @"
                INSERT INTO LogReprocesos (
                    id_Reprocesado,
                    usuario,
                    FechaReproceso,
                    Proceso
                ) VALUES (
                    @id,
                    @usuario,
                    DATEADD(hour, -6, GETDATE()),
                    @proceso
                );
            ";
                    using (var cmdIns = new SqlCommand(sqlInsert, conn))
                    {
                        cmdIns.Parameters.AddWithValue("@id", id);
                        cmdIns.Parameters.AddWithValue("@usuario", usuario);
                        cmdIns.Parameters.AddWithValue("@proceso", proceso);
                        cmdIns.ExecuteNonQuery();
                    }

                    procesados++;
                }

                return Ok(new { message = $"Se reprocesaron {procesados} cuentas correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al reprocesar masivamente.", detail = ex.Message });
            }
        }



        [HttpPost]
        [Route("cambiarStatusIndividual")]
        public IActionResult CambiarStatusIndividual([FromBody] JsonObject info)
        {
            try
            {
                // 1) Validación básica
                if (info == null
                    || info["id"] == null
                    || info["proceso"] == null
                    || info["status"] == null
                    || info["usuario"] == null)
                {
                    return BadRequest(new { message = "Petición inválida." });
                }

                int id = info["id"]!.GetValue<int>();
                string proceso = info["proceso"]!.GetValue<string>();
                string status = info["status"]!.GetValue<string>();
                string usuario = info["usuario"]!.GetValue<string>();

                // 1b) Lista blanca de tablas permitidas (opcional, pero muy recomendable)
                var tablasPermitidas = new[] { "AjustesSinValidacion", "okCliente2", "OrdenTroubleCall", "EjecucionNotDone", "CancelacionSinValidacion", "CasosNegocioSinValidacion", "AjustesBasesCasosNeogcioCobranza", "AjustesCambioServicios", "NotDoneCreacionOrdenModel", "DepuracionBasesCanceladasOSExt", "DepuracionBasesCanceladasOS" };

                if (!tablasPermitidas.Contains(proceso))
                {
                    return BadRequest(new { message = $"El proceso '{proceso}' no está permitido." });
                }

                // 2) Abrimos conexión
                using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
                conn.Open();

                // 3) UPDATE dinámico en la tabla correspondiente
                var sqlUpdate = $@"
            UPDATE [{proceso}]
            SET Status = @status
            WHERE Id = @id;
        ";
                using (var cmd = new SqlCommand(sqlUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                // 4) INSERT en LogCambioStatus
                var sqlInsert = @"
            INSERT INTO LogCambioStatus (
                id_Cambiado,
                usuario,
                FechaCambio,
                Proceso
            ) VALUES (
                @id,
                @usuario,
                DATEADD(hour, -6, GETDATE()),
                @proceso
            );
        ";
                using (var cmd = new SqlCommand(sqlInsert, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@proceso", proceso);
                    cmd.ExecuteNonQuery();
                }

                return Ok(new { message = "Status cambiado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cambiar status.", detail = ex.Message });
            }
        }


        [HttpPost]
        [Route("cambiarStatusMasivo")]
        public IActionResult CambiarStatusMasivo([FromBody] JsonObject info)
        {
            try
            {
                // 1) Validación básica
                if (info == null
                    || info["registros"] == null
                    || info["usuario"] == null)
                {
                    return BadRequest(new { message = "Petición inválida." });
                }

                // 2) Extraer parámetros
                string usuario = info["usuario"]!.GetValue<string>();
                string jsonRegistros = info["registros"]!.ToJsonString();

                // 3) Llamar al SP en un solo batch
                using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
                using var cmd = new SqlCommand("dbo.CambiarStatusMasivo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@jsonRegistros", jsonRegistros);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Ok(new { message = "Status masivo cambiado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al cambiar status masivo.", detail = ex.Message });
            }
        }

        
        [HttpPost("InsertarFlagConfirmacion")]
        public async Task<IActionResult> InsertarFlagConfirmacion([FromBody] List<flagConfirmacion> data)
        {
            if (data == null || !data.Any())
                return BadRequest("No se recibió información.");

            string jsonData = System.Text.Json.JsonSerializer.Serialize(data);

            using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            using var cmd = new SqlCommand("InsertarFlagConfirmacion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@json", SqlDbType.NVarChar).Value = jsonData;

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return Ok(new { message = "Datos insertados correctamente." });
        }

        [HttpGet]
        [Route("getAjustesFlagConfirmacion")]
        public async Task<ActionResult<IEnumerable<flagConfirmacion>>> getAjustesFlagConfirmacion()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var datos = _context.flagConfirmacion.FromSqlRaw($"select * from flagConfirmacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}' order by FechaCaptura desc;").ToList();
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
        [Route("statsBasesFlagConfirmacion")]
        public ArrayList statsBasesFlagConfirmacion()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            string sql = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='No aplica ajuste reciente' or status='Cerrada' or status='Completado' or status='Completada' or status='Caso de Negocio YA Tipificado' or status='Aplicación correcta' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM flagConfirmacion where CONVERT(date,FechaCaptura) between '{Date}' and '{Date}';";
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

        [HttpGet]
        [Route("getCuentaFlagConfirmacion")]
        public async Task<ActionResult<IEnumerable<flagConfirmacion>>> getCuentaFlagConfirmacion()
        {
            try
            {
                var datos = _context.flagConfirmacion.FromSqlRaw("exec Sp_getCuentaFlagConfirmacion").ToList();
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


        [Route("ActualizaFlagConfirmacion")]
        [HttpPut]
        public dynamic ActualizaFlagConfirmacion(int id, [FromBody] flagConfirmacion Cuenta)
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

        

    }
}
