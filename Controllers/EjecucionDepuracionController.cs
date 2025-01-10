using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Route("Reporte")]
    [Route("[controller]")]
    [ApiController]

    public class EjecucionDepuracionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EjecucionDepuracionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getHoursDepuracion")]
        public async Task<ActionResult<IEnumerable<HorariosReporte>>> getHoursDepuracion()
        {
            try
            {
                var datos = _context.HorariosReporte.FromSqlRaw("select * from HorariosReporte order by Horario asc;").ToList();
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
        [Route("GuardarHoursDepuracion")]
        public dynamic GuardarHoursDepuracion(HorariosReporte Cuenta)
        {

            try
            {
                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizaHoursDepuracion")]
        [HttpPut]
        public dynamic ActualizaHoursDepuracion(int id, [FromBody] HorariosReporte Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
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

        [Route("EliminarHoursDepuracion")]
        [HttpDelete]
        public dynamic EliminarHoursDepuracion(int id, [FromBody] HorariosReporte Cuenta)
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
        [Route("getEjecucionDepuracion")]
        public async Task<ActionResult<IEnumerable<EjecucionDepuracionModel>>> getEjecucionDepuracion()
        {
            try
            {
                var datos = _context.EjecucionDepuracion.FromSqlRaw("select * from EjecucionDepuracion order by FechaExtraccion desc;").ToList();
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
        [Route("GuardarFormularioEjecucionDepuracion")]
        public dynamic guardarFormularioEjecucionDepuracion(EjecucionDepuracionModel Cuenta)
        {
            try
            {
                Cuenta.FechaExtraccion = DateTime.Now;
                Cuenta.IP = "";
                Cuenta.Procesando = "1";

                _context.Add(Cuenta);
                _context.SaveChanges();

                return Ok(new { Id = Cuenta.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarEjecucionDepuracion")]
        [HttpPut]
        public dynamic ActualizarEjecucionDepuracion(int id, [FromBody] EjecucionDepuracionModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
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

        [HttpPost]
        [Route("GuardarFormularioBasesCC")]
        public dynamic GuardarFormularioBasesCC(EjecucionBasesCCModel Cuenta)
        {

            try
            {
                Cuenta.Status = "Completado";
                Cuenta.FechaCreacion = DateTime.Now;

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getBasesCC")]
        public async Task<ActionResult<IEnumerable<EjecucionBasesCCModel>>> getBasesCC()
        {
            try
            {
                var datos = _context.BasesCC.FromSqlRaw("select * from BasesCC order by FechaCreacion desc;").ToList();
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

        [Route("ActualizarBasesCC")]
        [HttpPut]
        public dynamic ActualizarBasesCC(int id, [FromBody] EjecucionBasesCCModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
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
        [Route("getBasesDepuracion")]
        public async Task<ActionResult<IEnumerable<BasesDepuracion>>> getBasesDepuracion()
        {
            try
            {
                var datos = _context.BasesDepuracion.FromSqlRaw("select * from BasesDepuracion order by Fecha_Registro desc;").ToList();
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
        [Route("GuardarFormularioBasesDepuracion")]
        public dynamic guardarFormularioBasesDepuracion(BasesDepuracion Cuenta)
        {

            try
            {
                //Cuenta.Status = "Pendiente";
                //Cuenta.FechaExtraccion = DateTime.Now;
                //Cuenta.IP = "";
                //Cuenta.Procesando = "0";

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarBasesDepuracion")]
        [HttpPut]
        public dynamic ActualizarBasesDepuracion(int id, [FromBody] BasesDepuracion Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
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

        [Route("BajarExcelFTPArchivosCC")]
        [HttpGet]
        public IActionResult BajarExcelFTPArchivosCC(string nombre)
        {
            string ftpServerIP = "192.168.50.37"; // Dirección IP del servidor FTP
            string ftpUserName = "rpaback1"; // Usuario del servidor FTP
            string ftpPassword = "Cyber123"; // Contraseña del usuario del servidor FTP

            // Nombre y ruta del archivo CSV a descargar
            string remoteFilePath = $"/BasesCC/{nombre}";

            // Crear una solicitud FTP para descargar el archivo
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // Obtener la respuesta del servidor FTP y guardar el archivo descargado en un MemoryStream
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        // Devolver el archivo CSV como un FileResult utilizando un MemoryStream
                        return File(fileBytes, "application/zip", nombre);
                    }
                }
            }
        }

        [Route("BajarExcelFTPExtraccionesDepuracion")]
        [HttpGet]
        public IActionResult BajarExcelFTPExtraccionesDepuracion(string nombre)
        {
            string ftpServerIP = "192.168.50.37"; // Dirección IP del servidor FTP
            string ftpUserName = "rpaback1"; // Usuario del servidor FTP
            string ftpPassword = "Cyber123"; // Contraseña del usuario del servidor FTP

            // Nombre y ruta del archivo CSV a descargar
            string remoteFilePath = $"/ExtraccionesDepuracion/{nombre}";

            // Crear una solicitud FTP para descargar el archivo
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // Obtener la respuesta del servidor FTP y guardar el archivo descargado en un MemoryStream
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        // Devolver el archivo CSV como un FileResult utilizando un MemoryStream
                        return File(fileBytes, "application/zip", nombre);
                    }
                }
            }
        }


        [Route("BajarExcelFTPBasesDepuradas")]
        [HttpGet]
        public IActionResult BajarExcelFTPBasesDepuradas(string nombre)
        {
            string ftpServerIP = "192.168.50.37"; // Dirección IP del servidor FTP
            string ftpUserName = "rpaback1"; // Usuario del servidor FTP
            string ftpPassword = "Cyber123"; // Contraseña del usuario del servidor FTP

            // Nombre y ruta del archivo CSV a descargar
            string remoteFilePath = $"/BasesDepuradas/{nombre}";

            // Crear una solicitud FTP para descargar el archivo
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            // Obtener la respuesta del servidor FTP y guardar el archivo descargado en un MemoryStream
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        // Devolver el archivo CSV como un FileResult utilizando un MemoryStream
                        return File(fileBytes, "application/zip", nombre);
                    }
                }
            }
        }

        [Route("InsertarBasesCanceladasExcel")]
        [HttpPost]
        public dynamic InsertarBasesCanceladasExcel([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "insert into DepuracionBasesCanceladasOs (Comentarios,Estado,Hub,MotivoOrden,Nodo,Cuenta,NumOrden,Tipo,FechaCarga,FechaCreado,Cve_usuario,Status,Procesando,IP,Compania) values ('" + data["Comentarios"] +"','" + data["Estado"] +"','" + data["Hub"] +"','" + data["Motivo de la orden"] +"','" + data["Nodo"] +"','" + data["Nº de cuenta"] +"','" + data["Nº de orden"] +"','" + data["Tipo"] +"','" + data["Fecha_Carga"] +"','" + data["Creado"] +"','" + data["Cve_usuario"] +"','" + data["Status"] +"','" + data["Procesando"] +"','" + data["IP"] +"','" + data["Compañía"] +"')";
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
        [Route("getCuentaDepuracionBasesCanceladasCC")]
        public async Task<ActionResult<IEnumerable<EjecucionDepuracionBasesCanceladasOs>>> getCuentaDepuracionBasesCanceladasCC()
        {
            try
            {
                var datos = _context.DepuracionBasesCanceladasOs.FromSqlRaw("exec Sp_getCuentaDepuracionBasesCanceladasCC").ToList();
                if (datos.Count() > 0)
                {
                    var data = new
                    {
                        status = true,
                        info = datos,
                        access_data = "[{'user':'p-jlima','password':'sobu*8vlDros'}]",
                        datos_os = new[]
                        {
                            new
                            {
                                Motivo_de_Cancelacion = "LP Cliente confirma servicio",
                                Estado="Cancelado",
                                Comentarios="Depuracion outbound cliente Confirma servicio Correcto se cancela OS, CERRADA POR DEPURACION CC"

                            }
                        },
                        datos_cn = new[]
                        {
                            new
                            {
                                Medio_de_contacto = "Outbound",
                                Categoría="OUTBOUND",
                                Motivos="SEGUIMIENTOS ESPECIALES",
                                Submotivo="LIMPIEZA DE POOL",
                                Solución="ORDEN YA COMPLETA",
                                Motivo_del_cierre="BO CONFIRMA Y SOLUCIONA",
                                Estado="Cerrado",
                                Comentarios=$"Depuración Outbound, OS: '{datos[0].NumOrden}', Tel Marcado: '',Quien Responde: Titular, Nombre: '{datos[0].Compania}', Resultado de la llamada: Se cancela OS, Cliente Confirma servicio Correcto, CERRADA POR DEPURACION CC"
                            }
                        },
                     };
                    return Ok(data);

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
        [Route("getCuentaDepuracionBasesCanceladasEXT")]
        public async Task<ActionResult<IEnumerable<EjecucionDepuracionBasesCanceladasOsEXT>>> getCuentaDepuracionBasesCanceladasEXT()
        {
            try
            {
                var datos = _context.DepuracionBasesCanceladasOsExt.FromSqlRaw("exec Sp_getCuentaDepuracionBasesCanceladasEXT").ToList();
                if (datos.Count() > 0)
                {
                    var data = new
                    {
                        status = true,
                        info = datos,
                        access_data = "[{'user':'p-lggarciah','password':'sobu*8vlDros'}]",
                        datos_os = new[]
                     {
                            new
                            {
                                Motivo_de_Cancelacion = "LP Cliente confirma servicio",
                                Estado="Cancelado",
                                Comentarios="Depuracion outbound cliente Confirma servicio Correcto se cancela OS, CERRADA POR DEPURACION EXT"

                            }
                        },
                        datos_cn = new[]
                     {
                            new
                            {
                                Medio_de_contacto = "Outbound",
                                Categoría="OUTBOUND",
                                Motivos="SEGUIMIENTOS ESPECIALES",
                                Submotivo="LIMPIEZA DE POOL",
                                Solución="ORDEN YA COMPLETA",
                                Motivo_del_cierre="BO CONFIRMA Y SOLUCIONA",
                                Estado="Cerrado",
                                Comentarios=$"Depuración Outbound, OS: '{datos[0].NumOrden}', Tel Marcado: '',Quien Responde: Titular, Nombre: '{datos[0].Compania}', Resultado de la llamada: Se cancela OS, Cliente Confirma servicio Correcto, CERRADA POR DEPURACION EXT"
                            }
                        },
                    };
                    return Ok(data);

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

        [Route("ActualizaDepuracionBasesCanceladasCC")]
        [HttpPut]
        public dynamic ActualizaDepuracionBasesCanceladasCC(int id, [FromBody] EjecucionDepuracionBasesCanceladasOs Cuenta)
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

        [Route("ActualizaDepuracionBasesCanceladasEXT")]
        [HttpPut]
        public dynamic ActualizaDepuracionBasesCanceladasEXT(int id, [FromBody] EjecucionDepuracionBasesCanceladasOsEXT Cuenta)
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
        [Route("statsBasesCanceladas")]
        public ArrayList statsBasesCanceladas()
        {
            var Date = DateTime.Now.Date.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
          
             string sql = $"SELECT SUM(CASE WHEN Status = 'Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status= 'Orden no valida' or Status='Erro al cancelar Orden' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Orden Cancelado por un tercero' or Status='Orden cancelado por RPA' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status like '%Procesando%' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total, 'CC' AS base FROM DepuracionBasesCanceladasOs where CONVERT(date,FechaCarga) between '{Date}' and '{Date}' union all SELECT SUM(CASE WHEN Status = 'Pendiente' or Status='NO' or Status='Registro pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status='Orden no valida' or Status='Erro al cancelar Orden' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Si' or Status='Orden Cancelado por un tercero' or Status='Orden cancelado por RPA' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status like '%Procesando%' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total, 'EXT' AS base FROM DepuracionBasesCanceladasOsExt where CONVERT(date,time_carga) between '{Date}' and '{Date}';";
            conn.Open();
             SqlCommand cmd = new SqlCommand(sql, conn);
             SqlDataReader reader = cmd.ExecuteReader();
             ArrayList objs = new ArrayList();
            while (reader.Read())
            {
                objs.Add(new
                {
                    Pendientes = reader["Pendientes"],
                    Base= reader["base"],
                    Error= reader["Error"],
                    Completado= reader["Completado"],
                    Procesando= reader["Procesando"],
                    Total= reader["Total"],
                });
            }

             conn.Close();
             return objs;
        

        }
        [Route("getConsultaBasesCanceladasOSEXT")]
        [HttpGet]
        public dynamic getConsultaBasesCanceladasOSEXT(string orden)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
                conn.Open();
                string sql = "SELECT TOP(1) X.* FROM (\r\n\tselect FechaCarga,Comentarios,Hub,MotivoOrden,Nodo,Cuenta,Estado,NumOrden,Tipo,Cve_usuario,Status,Procesando,Compania,'CC' as base from DepuracionBasesCanceladasOs where NumOrden='"+orden+"' \r\n    UNION ALL\r\n\tselect time_carga,Comentarios,Hub,MotivoOrden,Nodo,Cuenta,null as Estado,NumOrden,Tipo,Cve_usuario,Status,Procesando,Compania,'EXT' as base from DepuracionBasesCanceladasOsExt where NumOrden='"+orden+"' \r\n    \r\n) AS X;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ArrayList objs = new ArrayList();
                while (reader.Read())
                {
                    objs.Add(new
                    {
                        FechaCarga = reader["FechaCarga"],
                        Comentarios = reader["Comentarios"],
                        Hub = reader["Hub"],
                        MotivoOrden = reader["MotivoOrden"],
                        Nodo = reader["Nodo"],
                        Cuenta = reader["Cuenta"],
                        Estado = reader["Estado"],
                        NumOrden = reader["NumOrden"],
                        Tipo = reader["Tipo"],
                        Cve_Usuario = reader["Cve_Usuario"],
                        Status = reader["Status"],
                        Procesando = reader["Procesando"],
                        Compania = reader["Compania"],
                        Base = reader["base"],
                    });
                }
                conn.Close();
                return objs;

        }

        [HttpGet]
        [Route("getTablaDepuracionOS")]
        public async Task<ActionResult<IEnumerable<EjecucionDepuracionBasesCanceladasOs>>> getTablaDepuracionOS()
        {
            try
            {
                var datos = _context.DepuracionBasesCanceladasOs.FromSqlRaw("select * from DepuracionBasesCanceladasOs order by FechaCarga desc;").ToList();
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
        [Route("getTablaDepuracionEXT")]
        public async Task<ActionResult<IEnumerable<EjecucionDepuracionBasesCanceladasOsEXT>>> getTablaDepuracionEXT()
        {
            try
            {
                var datos = _context.DepuracionBasesCanceladasOsExt.FromSqlRaw("select * from DepuracionBasesCanceladasOsExt ORDER by time_carga desc;").ToList();
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
        [Route("getPrefijosRegionDepuracion")]
        public async Task<ActionResult<IEnumerable<PrefijosRegionDepuracion>>> getPrefijosRegionDepuracion()
        {
            try
            {
                var datos = _context.PrefijosRegionDepuracion.FromSqlRaw("select * from PrefijosRegionDepuracion;").ToList();
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
        [Route("guardarPrefijosRegionDepuracion")]
        public dynamic guardarPrefijosRegionDepuracion(PrefijosRegionDepuracion Cuenta)
        {

            try
            {
                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarPrefijosRegionDepuracion")]
        [HttpPut]
        public dynamic ActualizarPrefijosRegionDepuracion(int id, [FromBody] PrefijosRegionDepuracion Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
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




        [Route("EliminarPrefijosRegionDepuracion")]
        [HttpDelete]
        public dynamic EliminarPrefijosRegionDepuracion(int id, [FromBody] PrefijosRegionDepuracion Cuenta)
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




















    }
}
