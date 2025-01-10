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
    public class AjustesCambiosServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
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



        [Route("pruebaFechaHora")]
        [HttpGet]
        public dynamic pruebaFechaHora()
        {
            DateTime Date = DateTime.Now;
            ArrayList objs = new ArrayList();
                objs.Add(new
                {
                    fecha = Date,
                });
            return objs;

        }








    }
}
