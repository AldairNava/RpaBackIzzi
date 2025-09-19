using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.Json.Nodes;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;
using TriggerBuilder = Quartz.TriggerBuilder;
using ITrigger = Quartz.ITrigger;
using Quartz.Impl.Matchers;
using Mysqlx;
using Newtonsoft.Json;
using Quartz.Spi;
using DocumentFormat.OpenXml.InkML;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;



namespace WebApplication1.Controllers
{
    [Route("Reporte")]
    //[Route("[controller]")]
    [ApiController]


    public class EjecucionReporte : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IScheduler _scheduler;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EjecucionReporte(ApplicationDbContext context, IScheduler scheduler, IConfiguration configuration)
        {
            _context = context;
            _scheduler = scheduler;
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        [Route("vici")]
        public ArrayList vici()
        {
            ArrayList objs = new ArrayList();
            string connstring = "Server=192.168.50.221;Port=3306;database=asterisk;uid=cyberhub;pwd=de5yHdrEscF";
            MySqlConnection conn = new MySqlConnection(connstring);
            conn.ConnectionString = connstring;
            conn.Open();
            string query = "select list_id,list_name,list_description from vicidial_lists where  Year(list_lastcalldate)='2023' and campaign_id='FideiZZi' ;";
            using var command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objs.Add(new
                {
                    list_id = reader["list_id"],
                    list_name = reader["list_name"],
                    list_description = reader["list_description"],
                });
            }
            conn.Close();
            return objs;

        }

        [HttpGet]
        [Route("vici1")]
        public ArrayList vici1()
        {
            ArrayList objs = new ArrayList();
            //string connstring = "Server=192.168.50.221;Port=3306;database=asterisk;uid=cyberhub;pwd=de5yHdrEscF";
            string connstring = "Server=192.168.50.13;Port=3306;database=asterisk;uid=estrella_de_mar;pwd=dFr5Ty67%$ffddcgnG";
            MySqlConnection conn = new MySqlConnection(connstring);
            conn.ConnectionString = connstring;
            conn.Open();
            string query = "select list_id,list_name,list_description from vicidial_lists_azure;";
            using var command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                objs.Add(new
                {
                    list_id = reader["list_id"],
                    list_name = reader["list_name"],
                    list_description = reader["list_description"],
                });
            }
            conn.Close();
            return objs;

        }

        [HttpGet]
        [Route("getCuentaReporte")]
        public async Task<ActionResult<IEnumerable<EjecucionReporteModel>>> getCuentaReporte()
        {
            try
            {
                var datos = _context.EjecucionReporte.FromSqlRaw("exec Sp_getCuentaEjecucionReporte").ToList();
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
        [Route("getCuentaExtraccion")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccion>>> getCuentaExtraccion()
        {
            try
            {
                var datos = _context.EjecucionExtraccion.FromSqlRaw("exec Sp_getCuentaEjecucionExtraccion").ToList();
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
        [Route("getmostrarTablaExtraccion")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccion>>> mostrarExtraccion()
        {
            try
            {
                var datos = _context.EjecucionExtraccion.FromSqlRaw("select * from EjecucionExtraccion order by FechaExtraccion desc;").ToList();
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
        [Route("GuardarFormularioEjecucionExtraccion")]
        public dynamic guardarRegistroExtraccion(EjecucionExtraccion Cuenta)
        {

            try
            {
                Cuenta.Status = "Pendiente";
                Cuenta.FechaExtraccion = DateTime.Now;
                Cuenta.IP = "";
                Cuenta.Procesando = "0";

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizaEjecucionExtraccion")]
        [HttpPut]
        public dynamic ActualizaEjecucionExtraccion(int id, [FromBody] EjecucionExtraccion Cuenta)
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
        [Route("getRowsReporte")]
        public async Task<ActionResult<IEnumerable<EjecucionReporteModel>>> getRowsReporte()
        {
            try
            {
                var datos = _context.EjecucionReporte.FromSqlRaw("select * from EjecucionReporte order by FechaCaptura desc;").ToList();
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
        [Route("GuardarFormularioEjecucionReporte")]
        public dynamic guardarRegistroReporte(EjecucionReporteModel Cuenta)
        {

            try
            {
                Cuenta.Status = "Pendiente";
                Cuenta.FechaCaptura = DateTime.Now;
                Cuenta.IP = "";
                Cuenta.Procesando = "0";

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizaEjecucionReporte")]
        [HttpPut]
        public dynamic ActualizaEjecucionReporte(int id, [FromBody] EjecucionReporteModel Cuenta)
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

        [Route("BajarExtraccionExcelFTP")]
        [HttpGet]
        public async Task<IActionResult> BajarExtraccionExcelFTP(string nombre)
        {
            string ftpServerIP = "192.168.50.37"; // Dirección IP del servidor FTP
            string ftpUserName = "rpaback1"; // Usuario del servidor FTP
            string ftpPassword = "Cyber123"; // Contraseña del usuario del servidor FTP

            // Nombre y ruta del archivo CSV a descargar

            string remoteFilePath = $"/Extracciones/{nombre}";
            int requestTimeout = _configuration.GetValue<int>("RequestTimeoutSettings:TimeoutInSeconds");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            request.Timeout = requestTimeout * 1000;

            try
            {
                FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
                return new FtpStreamResult(response, "application/zip") { FileDownloadName = nombre };
            }
            catch (Exception ex)
            {
                // Puedes loggear o manejar la excepción según necesites
                return StatusCode(500, "Error al conectar o recibir respuesta del servidor FTP.");
            }
        }

        public class FtpStreamResult : FileStreamResult
        {
            private FtpWebResponse _ftpResponse;

            public FtpStreamResult(FtpWebResponse ftpResponse, string contentType)
                : base(ftpResponse.GetResponseStream(), contentType)
            {
                _ftpResponse = ftpResponse;
            }

            public override void ExecuteResult(ActionContext context)
            {
                base.ExecuteResult(context);
                _ftpResponse.Close();
            }

            public async override Task ExecuteResultAsync(ActionContext context)
            {
                await base.ExecuteResultAsync(context);
                _ftpResponse.Close();
            }
        }

        [Route("BajarExcelFTPReporteFidelizacion")]
        [HttpGet]
        public IActionResult BajarExcelFTPReporteFidelizacion(string nombre)
        {
            string ftpServerIP = "192.168.50.37"; // Dirección IP del servidor FTP
            string ftpUserName = "rpaback1"; // Usuario del servidor FTP
            string ftpPassword = "Cyber123"; // Contraseña del usuario del servidor FTP

            // Nombre y ruta del archivo CSV a descargar
            string remoteFilePath = $"/ReporteFidelizacion/{nombre}";
            int requestTimeout = _configuration.GetValue<int>("RequestTimeoutSettings:TimeoutInSeconds");

            // Asegurarnos de que el tiempo de espera sea mayor que 0
            if (requestTimeout <= 0)
            {
                requestTimeout = 300; // Establecer un valor predeterminado (en segundos) si el tiempo de espera no es válido
            }

            // Crear una solicitud FTP para descargar el archivo
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // Ajustar el tiempo de espera (en milisegundos)
            request.Timeout = requestTimeout * 1000;
            request.ReadWriteTimeout = requestTimeout * 1000;

            try
            {
                // Obtener la respuesta del servidor FTP y guardar el archivo descargado en un MemoryStream
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (System.IO.Stream responseStream = response.GetResponseStream())
                    {
                        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                        {
                            responseStream.CopyTo(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();

                            // Devolver el archivo como un FileResult utilizando un MemoryStream
                            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    // Manejar error de tiempo de espera
                    return StatusCode((int)HttpStatusCode.RequestTimeout, "La operación ha excedido el tiempo de espera.");
                }
                else if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    // Manejar error de conexión al FTP
                    return StatusCode((int)HttpStatusCode.InternalServerError, "No se pudo conectar al servidor FTP. Verifique la conexión de red.");
                }
                else if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    // Manejar error de protocolo (posiblemente credenciales incorrectas)
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.NotLoggedIn)
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized, "Las credenciales del servidor FTP son incorrectas.");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, "Error de protocolo FTP: " + response.StatusDescription);
                    }
                }
                else
                {
                    // Manejar otros errores de red
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }






        [HttpPost]
        [Route("GuardarFormularioEjecucionExtraccionAutomatizados")]
        public dynamic GuardarFormularioEjecucionExtraccionAutomatizados(EjecucionExtraccionAutomatizados Cuenta)
        {

            try
            {
                Cuenta.Status = "Pendiente";
                Cuenta.FechaExtraccion = DateTime.Now;
                Cuenta.IP = "";
                Cuenta.Procesando = "0";

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarFormularioEjecucionExtraccionAutomatizadosPrueba")]
        public dynamic GuardarFormularioEjecucionExtraccionAutomatizadosPrueba(EjecucionExtraccionAutomatizadosPrueba Cuenta)
        {

            try
            {
                Cuenta.Status = "Pendiente";
                Cuenta.FechaExtraccion = DateTime.Now;
                Cuenta.IP = "";
                Cuenta.Procesando = "0";

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarFormularioEjecucionExtraccionAutomatizados2")]
        public dynamic GuardarFormularioEjecucionExtraccionAutomatizados2(EjecucionExtraccionAutomatizados2 Cuenta)
        {

            try
            {
                //Cuenta.Status = "Completado";
                Cuenta.FechaExtraccion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizaEjecucionExtraccionAutomatizados")]
        [HttpPut]
        public dynamic ActualizaEjecucionExtraccionAutomatizados(int id, [FromBody] EjecucionExtraccionAutomatizados Cuenta)
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
        [Route("ActualizaEjecucionExtraccionAutomatizados2Prueba")]
        [HttpPut]
        public dynamic ActualizaEjecucionExtraccionAutomatizados2Prueba(int id, [FromBody] EjecucionExtraccionAutomatizados2Prueba Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    Cuenta.FechaCompletado = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
        [Route("getCuentaExtraccionAutomatizadas2prueba")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados2Prueba>>> getCuentaExtraccionAutomatizadas2prueba()
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados2Prueba.FromSqlRaw("exec Sp_getCuentaEjecucionExtraccionAutomatizados2Prueba").ToList();
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


        [Route("ActualizaEjecucionExtraccionAutomatizadosHoraProgramada")]
        [HttpPut]
        public dynamic ActualizaEjecucionExtraccionAutomatizadosHoraProgramada(int id, [FromBody] EjecucionExtraccionAutomatizados Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    var row = _context.EjecucionExtraccionAutomatizados.FirstOrDefault(b => b.Id == id);
                    if (row != null)
                    {

                            row.horaProgramacion = Cuenta.horaProgramacion;
                            //_context.Update(Cuenta);
                            _context.SaveChanges();
                            return Ok(Cuenta);

                    }
                    else
                    {
                        return NotFound();

                    }

                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizaEjecucionExtraccionAutomatizadosHoraProgramadaPrueba")]
        [HttpPut]
        public dynamic ActualizaEjecucionExtraccionAutomatizadosHoraProgramadaPrueba(int id, [FromBody] EjecucionExtraccionAutomatizadosPrueba Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    var row = _context.EjecucionExtraccionAutomatizadosPrueba.FirstOrDefault(b => b.Id == id);
                    if (row != null)
                    {
                        _context.Entry(row).CurrentValues.SetValues(Cuenta);
                        //_context.Update(Cuenta);
                        _context.SaveChanges();

                        return Ok(Cuenta);

                    }
                    else
                    {
                        return NotFound();

                    }

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
        [Route("validarEjecucionExtraccionAutomatizacionHoraProgramada")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados>>> validarEjecucionExtraccionAutomatizacionHoraProgramada(string hora,string id)
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados.FromSqlRaw($"SELECT * FROM EjecucionExtraccionAutomatizados where id!={id} and CONVERT(date,FechaExtraccion)=Convert(date,getdate()) and horaProgramacion like '{hora}%';").ToList();
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

        [Route("validarEjecucionExtraccionAutomatizacionHoraProgramadaPrueba")]
        [HttpPost]
        public dynamic validarEjecucionExtraccionAutomatizacionHoraProgramadaPrueba([FromBody] JsonObject Info) 
        {
            try
            {
                //var datos = _context.EjecucionExtraccionAutomatizadosPrueba.FromSqlRaw($"SELECT * FROM EjecucionExtraccionAutomatizadosPrueba where id!={id} and CONVERT(date,FechaExtraccion)=Convert(date,getdate()) and horaProgramacion like '{hora}%';").ToList();

                var datos = _context.EjecucionExtraccionAutomatizadosPrueba.FromSqlRaw(
                            "SELECT * FROM EjecucionExtraccionAutomatizadosPrueba WHERE tipoExtraccion = '" + Info["tipoExtraccion"] + "' AND Cve_usuario='" + Info["cve_usuario"] +"' AND tipoProgramacion= '" + Info["tipoProgramacion"] + "' AND medioExtraccion='" + Info["medioExtraccion"] + "' AND correo ='" + Info["correo"] + "' and horaProgramacion='" + Info["horaProgramacion"] +"' and schenduleExpression='" + Info["schenduleExpression"] +"';"
                        ).ToList();
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


                //return parametrosExtraccion;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("validarEjecucionExtraccionAutomatizacionHoraProgramada2")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados>>> validarEjecucionExtraccionAutomatizacionHoraProgramada2(string hora)
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados.FromSqlRaw($"SELECT * FROM EjecucionExtraccionAutomatizados where CONVERT(date,FechaExtraccion)=Convert(date,getdate()) and horaProgramacion like '{hora}%';").ToList();
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
        [Route("validarEjecucionExtraccionAutomatizacionHoraProgramada2Prueba")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizadosPrueba>>> validarEjecucionExtraccionAutomatizacionHoraProgramada2Prueba(string hora)
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizadosPrueba.FromSqlRaw($"SELECT * FROM EjecucionExtraccionAutomatizadosPrueba where CONVERT(date,FechaExtraccion)=Convert(date,getdate()) and horaProgramacion like '{hora}%';").ToList();
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

        [Route("EliminarEjecucionExtraccionAutomatizados")]
        [HttpDelete]
        public dynamic EliminarEjecucionExtraccionAutomatizados(int id, [FromBody] EjecucionExtraccionAutomatizados Cuenta)
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

        [Route("EliminarEjecucionExtraccionAutomatizadosPrueba")]
        [HttpDelete]
        public dynamic EliminarEjecucionExtraccionAutomatizadosPrueba(int id, [FromBody] EjecucionExtraccionAutomatizadosPrueba Cuenta)
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
        [HttpPost]
        [Route("agregarNuevoCron")]
        public async Task<IActionResult> AgregarNuevoCron([FromBody] object Info)
        {
            try
            {
                string apiUrl = "http://192.168.49.76:80/programar";
                //string requestBody = "{\"key\":\"value\"}"; // Cuerpo de la solicitud POST en formato JSON

                //var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                //string requestBody = JsonConvert.SerializeObject(Info);
                //var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                //var content = new StringContent(Info.ToString(), Encoding.UTF8, "application/json");
                string jsonString = "" + Info.ToString() + "";
                var content = new StringContent(jsonString,Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

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

        //[HttpPost]
        //[Route("agregarNuevoCron")]
        //public async Task<IActionResult> AgregarNuevoCron([FromBody] EjecucionExtraccionAutomatizados2Prueba model)
        //{
        //    try
        //    {

        //        //string jsonArrayString = model.JsonArray.ToString();
        //        string jsonArrayString = JsonConvert.SerializeObject(model);

        //        IJobDetail job = JobBuilder.Create<HelloWorldJob>()
        //            .WithIdentity(model.nombreCron, "group1")
        //            .UsingJobData("parametro1", jsonArrayString) // Pasar el JSON Array como cadena
        //            .StoreDurably()
        //            .Build();
        //        //await _scheduler.AddJob(job, true);
        //        ITrigger trigger = TriggerBuilder.Create()
        //            .ForJob(model.nombreCron, "group1")
        //            .WithIdentity(model.nombreCron, "group1")
        //            .StartNow()
        //            .WithCronSchedule(model.scheduleExpression)
        //            .Build();

        //        await _scheduler.ScheduleJob(job, trigger);

        //        return Ok("Nuevo cron agregado exitosamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("listarCron")]
        public async Task<IActionResult> ListarCron()
        {
            //try
            //{
            //    var triggerKeys = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
            //    var cronList = new List<string>();

            //    foreach (var triggerKey in triggerKeys)
            //    {
            //        var trigger = await _scheduler.GetTrigger(triggerKey);
            //        if (trigger is ICronTrigger cronTrigger)
            //        {
            //            cronList.Add($"Nombre: {trigger.Key.Name}, Grupo: {trigger.Key.Group}, Próxima ejecución: {cronTrigger.GetNextFireTimeUtc()?.LocalDateTime}");
            //        }
            //    }

            //    if (cronList.Count > 0)
            //    {
            //        return Ok(cronList);
            //    }
            //    else
            //    {
            //        return Ok("No hay trabajos programados (crons) existentes.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            try
            {
                string apiUrl = "http://192.168.49.76:80/consultar"; // URL del servidor HTTP
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Procesar la respuesta del servidor como sea necesario
                    return Ok(responseBody);
                }
                else
                {
                    return BadRequest((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete]
        //[Route("eliminarCron")]
        //public async Task<IActionResult> EliminarCron([FromBody] EjecucionExtraccionAutomatizadosPrueba model)
        //{
        //    try
        //    {
        //        await _scheduler.UnscheduleJob(new TriggerKey(model.nombreCron, "group1"));
        //        return Ok("Cron eliminado exitosamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        [Route("eliminarCron")]
        public async Task<IActionResult> EliminarCron([FromBody] object Info)
        {
            try
            {
                string apiUrl = "http://192.168.49.76:80/eliminar"; // URL del servidor para la solicitud POST

                //var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                //string requestBody = JsonConvert.SerializeObject(Info);
                //var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                string jsonString = "" + Info.ToString() + "";
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Procesar la respuesta del servidor como sea necesario
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

        [HttpGet]
        [Route("getmostrarTablaExtraccionAutomatizados")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados>>> getmostrarTablaExtraccionAutomatizados()
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados.FromSqlRaw("select * from EjecucionExtraccionAutomatizados order by FechaExtraccion desc;").ToList();
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
        [Route("getmostrarTablaExtraccionAutomatizadosPrueba")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizadosPrueba>>> getmostrarTablaExtraccionAutomatizadosPrueba(string usuario)
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizadosPrueba.FromSqlRaw($"select * from EjecucionExtraccionAutomatizadosPrueba where Cve_usuario='{usuario}' order by FechaExtraccion desc;").ToList();
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
        [Route("getmostrarTablaExtraccionAutomatizados2")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados2>>> getmostrarTablaExtraccionAutomatizados2()
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados2.FromSqlRaw("select * from EjecucionExtraccionAutomatizados2 order by FechaExtraccion desc;").ToList();
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
        [Route("getmostrarTablaExtraccionAutomatizados2Prueba")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados2Prueba>>> getmostrarTablaExtraccionAutomatizados2Prueba()
        {
            try
            {
                var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                var Date1 = DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
                var datos = _context.EjecucionExtraccionAutomatizados2Prueba.FromSqlRaw($"select * from EjecucionExtraccionAutomatizados2Prueba where convert(date,FechaExtraccion) between '{Date1}' and '{Date}' order by FechaExtraccion desc;").ToList();
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
        [Route("getExtraccionAutomatizadosAsc")]
        public async Task<ActionResult<IEnumerable<EjecucionExtraccionAutomatizados>>> getExtraccionAutomatizadosAsc()
        {
            try
            {
                var datos = _context.EjecucionExtraccionAutomatizados.FromSqlRaw("select * from EjecucionExtraccionAutomatizados order by horaProgramacion asc;").ToList();
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
        [Route("getMostrarCatalogoExtraccionAutomatizadasCategoria")]
        public async Task<ActionResult<IEnumerable<cat_extraccionesAutomatizadasModel>>> getMostrarCatalogoExtraccionAutomatizadasCategoria()
        {
            try
            {
                //var datos = _context.cat_extraccionesAutomatizadasModel.FromSqlRaw("select id,motivo from cat_extraccionesAutomatizadasModel order by motivo asc").ToList();
                var query = (from motivo in _context.cat_extraccionesAutomatizadasModel
                             orderby motivo.categoria ascending
                             select motivo.categoria).Distinct();
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
        [Route("getMostrarCatalogoExtraccionAutomatizadasMotivo")]
        public async Task<ActionResult<IEnumerable<cat_extraccionesAutomatizadasModel>>> getMostrarCatalogoExtraccionAutomatizadasMotivo(string categoria1)
        {
            try
            {
                //var datos = _context.cat_extraccionesAutomatizadasModel.FromSqlRaw("select id,motivo from cat_extraccionesAutomatizadasModel order by motivo asc").ToList();
                var query = (from motivo in _context.cat_extraccionesAutomatizadasModel
                             where motivo.categoria == categoria1
                             select motivo.motivo).Distinct();
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
        [Route("getMostrarCatalogoExtraccionAutomatizadasSubmotivoSolucion")]
        public async Task<ActionResult<IEnumerable<cat_extraccionesAutomatizadasModel>>> getMostrarCatalogoExtraccionAutomatizadasSubmotivoSolucion(string motivo1,string categoria)
        {
            try
            {
                ArrayList objs = new ArrayList();

                var querySolucion = (from motivo in _context.cat_extraccionesAutomatizadasModel
                                     where motivo.motivo == motivo1
                                     where motivo.categoria == categoria
                                     select motivo.solucion).Distinct().ToList();

                var querySubmotivo = (from motivo in _context.cat_extraccionesAutomatizadasModel
                                      where motivo.motivo == motivo1
                                      where motivo.categoria == categoria
                                      select motivo.submotivo).Distinct().ToList();

                objs.Add(new
                {
                    submotivo = querySubmotivo,
                    solucion = querySolucion,
                });

                var datos = objs;
                if (datos.Count > 0)
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
        [Route("getMostrarCatalogoExtraccionAutomatizadasMotivoCliente")]
        public async Task<ActionResult<IEnumerable<cat_extraccionesAutomatizadasModel>>> getMostrarCatalogoExtraccionAutomatizadasMotivoCliente(string motivo1, string submotivo, string solucion,string categoria)
        {
            try
            {
                ArrayList objs = new ArrayList();

                var queryMotivoCliente = (from motivo in _context.cat_extraccionesAutomatizadasModel
                                          where motivo.motivo == motivo1
                                          where motivo.submotivo == submotivo
                                          where motivo.solucion == solucion
                                          where motivo.categoria == categoria
                                          select motivo.motivosCliente).Distinct().ToList();


                objs.Add(new
                {
                    motivoCliente = queryMotivoCliente
                });

                var datos = objs;
                if (datos.Count > 0)
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

        [Route("InsertarBasescatalagoEjecucionExtraccionAutomatizadas")]
        [HttpPost]
        public dynamic InsertarBasescatalagoEjecucionExtraccionAutomatizadas([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into cat_extraccionesAutomatizadasModel (motivo,submotivo,solucion, motivosCliente,categoria) values ('" + data["Motivo"] + "','" + data["Submotivo"] + "','" + data["Solución"] + "','" + data["Motivos del cliente"] + "','" + data["Categoría"] + "')";
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
        [Route("getMostrarCatalogoExtraccionAutomatizadasOSACT")]
        public async Task<ActionResult<IEnumerable<cat_extraccionesAutomatizadasOSActModel>>> getMostrarCatalogoExtraccionAutomatizadasOSACT()
        {
            try
            {
                ArrayList objs = new ArrayList();

                var queryTipoOrden = (from tipoOrden in _context.cat_extraccionesAutomatizadasOSActModel
                                     where tipoOrden.tipoOrdenOS != null && tipoOrden.tipoOrdenOS != ""
                                     select tipoOrden.tipoOrdenOS).Distinct().ToList();

                var queryEstado = (from estado in _context.cat_extraccionesAutomatizadasOSActModel
                                      where estado.estadoOS != null && estado.estadoOS != ""
                                      select estado.estadoOS).Distinct().ToList();

                var queryTipo = (from tipo in _context.cat_extraccionesAutomatizadasOSActModel
                                   where tipo.tipoAct != null && tipo.tipoAct != ""
                                   select tipo.tipoAct).Distinct().ToList();

                var queryAreaConocimiento = (from area in _context.cat_extraccionesAutomatizadasOSActModel
                                   where area.areaConocimientoAct != null && area.areaConocimientoAct != ""
                                   select area.areaConocimientoAct).Distinct().ToList();

                var queryEstadoAct = (from estado in _context.cat_extraccionesAutomatizadasOSActModel
                                   where estado.estadoAct != null && estado.estadoAct != ""
                                   select estado.estadoAct).Distinct().ToList();

                objs.Add(new
                {
                    tipoOrden = queryTipoOrden,
                    estadoOS = queryEstado,
                    tipo = queryTipo,
                    area = queryAreaConocimiento,
                    estadoAct = queryEstadoAct,
                });

                var datos = objs;
                if (datos.Count > 0)
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
        [Route("cambiarProcesosIzziLimpieza")]
        public async Task<IActionResult> cambiarProcesosIzziLimpieza(string ip, string proceso, string status)
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

        [HttpGet]
        [Route("cambiarProcesosIzzi")]
        public async Task<IActionResult> cambiarProcesosIzzi(string ip , string proceso, string status)
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

        [HttpGet]
        [Route("enviarCorreoBotsContrasenas")]
        public async Task<IActionResult> enviarCorreoBotsContrasenas(string ip, string proceso, string status)
        {
            try
            {
                string apiUrl = "http://192.168.50.33/api/Dac/enviarEmailIzziContrasenas";


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









    }
    public class HelloWorldJob : IJob
    {

        private readonly ApplicationDbContext _context;
        private readonly IScheduler _scheduler;
        private readonly string _parametro1;



        public HelloWorldJob(ApplicationDbContext context, IScheduler scheduler, string parametro1)
        {
            _context = context;
            _scheduler = scheduler;
            _parametro1 = parametro1;   
        }

        public Task Execute(IJobExecutionContext context1)
        {
            try
            {
                if (_parametro1 != null)
                {
                    //List<EjecucionExtraccionAutomatizados2Prueba> listaModelos = JsonConvert.DeserializeObject<List<EjecucionExtraccionAutomatizados2Prueba>>(_parametro1);

                    //if (listaModelos.Count > 0)
                    //{
                    //    EjecucionExtraccionAutomatizados2Prueba modelo = listaModelos[0];

                    //    _context.Add(modelo);
                    //    _context.SaveChanges();
                    //}
                    JobDataMap dataMap = context1.JobDetail.JobDataMap;
                    string jobSays = dataMap.GetString("parametro1");
                    ArrayList objs = new ArrayList();
                    objs.Add(new
                    
                        {
                        id= 0,
                        tipoExtraccion= "string",
                        fechaExtraccion= "2023-08-21T15:58:56.580Z",
                        parametrosExtraccion= "string",
                        archivo= "string",
                        cve_usuario= "string",
                        fechaCompletado= "2023-08-21T15:58:56.580Z",
                        status= "string",
                        procesando= "string",
                        ip= "string",
                        horaProgramacion= "string",
                        nombreCron= "prueba",
                        scheduleExpression= "* 0/1 * * * ? *"

                });
                    var anonObject = objs[0]; // El objeto anónimo en el ArrayList

                    var modelo = new EjecucionExtraccionAutomatizados2Prueba
                    {
                        Id = (int)anonObject.GetType().GetProperty("id").GetValue(anonObject, null),
                        tipoExtraccion = (string)anonObject.GetType().GetProperty("tipoExtraccion").GetValue(anonObject, null),
                        //FechaExtraccion = DateTime.Parse((string)anonObject.GetType().GetProperty("fechaExtraccion").GetValue(anonObject, null)),
                        FechaExtraccion = (string)anonObject.GetType().GetProperty("fechaExtraccion").GetValue(anonObject, null),
                        ParametrosExtraccion = (string)anonObject.GetType().GetProperty("parametrosExtraccion").GetValue(anonObject, null),
                        Archivo = (string)anonObject.GetType().GetProperty("archivo").GetValue(anonObject, null),
                        Cve_usuario = (string)anonObject.GetType().GetProperty("cve_usuario").GetValue(anonObject, null),
                        //FechaCompletado = DateTime.Parse((string)anonObject.GetType().GetProperty("fechaCompletado").GetValue(anonObject, null)),
                        FechaCompletado = (string)anonObject.GetType().GetProperty("fechaCompletado").GetValue(anonObject, null),
                        Status = (string)anonObject.GetType().GetProperty("status").GetValue(anonObject, null),
                        Procesando = (string)anonObject.GetType().GetProperty("procesando").GetValue(anonObject, null),
                        IP = (string)anonObject.GetType().GetProperty("ip").GetValue(anonObject, null),
                        horaProgramacion = (string)anonObject.GetType().GetProperty("horaProgramacion").GetValue(anonObject, null),
                        nombreCron = (string)anonObject.GetType().GetProperty("nombreCron").GetValue(anonObject, null),
                        scheduleExpression = (string)anonObject.GetType().GetProperty("scheduleExpression").GetValue(anonObject, null)
                    };
                    _context.EjecucionExtraccionAutomatizados2Prueba.Add(modelo);
                    _context.SaveChanges();


                    //EjecucionExtraccionAutomatizados2Prueba modelo = JsonConvert.DeserializeObject<EjecucionExtraccionAutomatizados2Prueba>(objs);

                    //if (modelo != null)
                    //{
                    //    _context.Add(modelo);
                    //    _context.SaveChanges();
                    //}
                    return Task.CompletedTask;
                }
                else
                {
                    ArrayList objs = new ArrayList();
                    objs.Add(new

                    {
                        id = 0,
                        tipoExtraccion = "string",
                        fechaExtraccion = "2023-08-21T15:58:56.580Z",
                        parametrosExtraccion = "string",
                        archivo = "string",
                        cve_usuario = "string",
                        fechaCompletado = "2023-08-21T15:58:56.580Z",
                        status = "string",
                        procesando = "string",
                        ip = "string",
                        horaProgramacion = "string",
                        nombreCron = "prueba",
                        scheduleExpression = "* 0/1 * * * ? *"

                    });
                    var anonObject = objs[0]; // El objeto anónimo en el ArrayList

                    var modelo = new EjecucionExtraccionAutomatizados2Prueba
                    {
                        Id = (int)anonObject.GetType().GetProperty("id").GetValue(anonObject, null),
                        tipoExtraccion = (string)anonObject.GetType().GetProperty("tipoExtraccion").GetValue(anonObject, null),
                        //FechaExtraccion = DateTime.Parse((string)anonObject.GetType().GetProperty("fechaExtraccion").GetValue(anonObject, null)),
                        FechaExtraccion = (string)anonObject.GetType().GetProperty("fechaExtraccion").GetValue(anonObject, null),
                        ParametrosExtraccion = (string)anonObject.GetType().GetProperty("parametrosExtraccion").GetValue(anonObject, null),
                        Archivo = (string)anonObject.GetType().GetProperty("archivo").GetValue(anonObject, null),
                        Cve_usuario = (string)anonObject.GetType().GetProperty("cve_usuario").GetValue(anonObject, null),
                        //FechaCompletado = DateTime.Parse((string)anonObject.GetType().GetProperty("fechaCompletado").GetValue(anonObject, null)),
                        FechaCompletado = (string)anonObject.GetType().GetProperty("fechaCompletado").GetValue(anonObject, null),
                        Status = (string)anonObject.GetType().GetProperty("status").GetValue(anonObject, null),
                        Procesando = (string)anonObject.GetType().GetProperty("procesando").GetValue(anonObject, null),
                        IP = (string)anonObject.GetType().GetProperty("ip").GetValue(anonObject, null),
                        horaProgramacion = (string)anonObject.GetType().GetProperty("horaProgramacion").GetValue(anonObject, null),
                        nombreCron = (string)anonObject.GetType().GetProperty("nombreCron").GetValue(anonObject, null),
                        scheduleExpression = (string)anonObject.GetType().GetProperty("scheduleExpression").GetValue(anonObject, null)
                    };
                    _context.EjecucionExtraccionAutomatizados2Prueba.Add(modelo);
                    _context.SaveChanges();

                    return Task.CompletedTask;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Execute: " + ex.Message);
                return Task.FromException(ex);
            }
        }






    }


}
