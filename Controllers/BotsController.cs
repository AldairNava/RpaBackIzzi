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
    public class BotsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public BotsController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("getBotsLimpieza")]
        public async Task<ActionResult<IEnumerable<BotsModel>>> getBotsLimpieza()
        {
            try
            {
                var query = from proceso in _context.cat_procesosLimpieza
                            join bot in _context.BotsProcessLimpieza on proceso.Id equals bot.ProcesoBotId into botGroup
                            from bot in botGroup.DefaultIfEmpty()
                            select new
                            {
                                BotId = bot == null ? 0 : bot.Id,
                                BotComentarios = bot == null ? "N/A" : bot.comentarios,
                                BotHostName = bot == null ? "N/A" : bot.hostName,
                                BotFechaActualizacion = bot == null ? DateTime.MinValue : bot.fechaActualizacion,
                                BotIp = bot == null ? "N/A" : bot.ip,
                                BotEstado = bot == null ? "N/A" : bot.estado,
                                ProcesoId = proceso.Id,
                                ProcesoName = proceso.Name_process,
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                                ProcesoFechaActualizacion = proceso.update_At,
                                ProcesoStatus = proceso.Status,
                                Procesonombreuser = proceso.Name_usuario
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
        [Route("getBots")]
        public async Task<ActionResult<IEnumerable<BotsModel>>> getBots()
        {
            try
            {
                var query = from bot in _context.BotsProcess
                            join proceso in _context.cat_procesos on bot.ProcesoBotId equals proceso.Id
                            select new
                            {
                                BotId = bot.Id,
                                BotComentarios = bot.comentarios,
                                BotHostName = bot.hostName,
                                BotFechaActualizacion = bot.fechaActualizacion,
                                BotIp = bot.ip,
                                BotEstado= bot.estado,
                                ProcesoId = proceso.Id,
                                ProcesoName = proceso.Name_process,
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                                ProcesoFechaActualizacion = proceso.update_At,
                                ProcesoStatus = proceso.Status
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
        [Route("getBotsEstadoLimpieza")]
        public async Task<ActionResult<IEnumerable<BotsModel>>> getBotsEstadoLimpieza()
        {
            try
            {
                var query = from bot in _context.BotsProcessLimpieza

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
        [Route("getBotsEstado")]
        public async Task<ActionResult<IEnumerable<BotsModel>>> getBotsEstado()
        {
            try
            {
                //var datos = _context.BotsProcess.FromSqlRaw("select * from BotsProcess;").ToList();
                var query = from bot in _context.BotsProcess
                            
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
        [Route("getBotByIdLimpieza/{id}")]
        public async Task<ActionResult<BotsModel>> getBotByIdLimpieza(int id)
        {
            try
            {
                var query = from bot in _context.BotsProcessLimpieza
                            join proceso in _context.cat_procesosLimpieza on bot.ProcesoBotId equals proceso.Id
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

        [HttpGet]
        [Route("getBotById/{id}")]
        public async Task<ActionResult<BotsModel>> getBotById(int id)
        {
            try
            {
                var query = from bot in _context.BotsProcess
                            join proceso in _context.cat_procesos on bot.ProcesoBotId equals proceso.Id
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

        [HttpGet]
        [Route("getProcess")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getProcess(string ip)
        {
            try
            {
                var query = from bot in _context.BotsProcess
                            join proceso in _context.cat_procesos on bot.ProcesoBotId equals proceso.Id
                            where bot.ip == ip
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

      [HttpGet]
[Route("getProcessLimpieza")]
public async Task<ActionResult<IEnumerable<catalogoProcesosBotsLimpiezaModel>>> getProcessLimpieza(string ip)
{
    try
    {
        var query = from proceso in _context.cat_procesosLimpieza
                    select new
                    {
                        ProcesoName = proceso.Name_process,
                        ProcesoUser = proceso.usuario,
                        ProcesoPassword = proceso.password,
                        Procesonombreuser = proceso.Name_usuario
                    };

        var datos = await query.ToListAsync();
        if (datos != null && datos.Count > 0)
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
        [Route("getUsrbots")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getUsrbots()
        {
            try
            {
                var query = from proceso in _context.cat_procesos
                            select new
                            {
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                                ProcesoUpdateAt = proceso.update_At,
                                Procesoasignado = proceso.Name_process,
                                Id=proceso.Id
                            };

                var datos = await query.ToListAsync();
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

        [HttpPost]
        [Route("SendMail")]
        public IActionResult SendMail([FromBody] MailRequest request)
        {
            try
            {
                var smtpClient = new SmtpClient("mail.cyberideas.com.mx")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("rpanotificacionescx@cyberideas.com.mx", "q5Ph6ra4Kd"),
                    EnableSsl = true,
                };

                smtpClient.Send("rpanotificacionescx@cyberideas.com.mx", request.To, request.Subject, request.Body);

                return Ok("Correo enviado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarUsuariobots")]
        [HttpDelete]
        public dynamic EliminarUsuariobots(int id, [FromBody] catalogoProcesosBotsModel fila)
        {
            try
            {
                if (id == fila.Id)
                {
                    _context.Remove(fila);
                    _context.SaveChanges();
                    return Ok(fila);
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
        [Route("GuardarBots")]
        public dynamic GuardarBots(BotsModel Cuenta)
        {

            try
            {
                Cuenta.created_at = DateTime.Now;

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarUsuariobots")]
        public dynamic ActualizarUsuariobots(int id, [FromBody] catalogoProcesosBotsModel fila)
        {
            try
            {
                var usuarioBot = _context.cat_procesos.Find(id);
                if (usuarioBot != null)
                {
                    // Actualiza los campos del usuarioBot con los valores de fila
                    usuarioBot.usuario = fila.usuario;
                    usuarioBot.password = fila.password;
                    usuarioBot.update_At = DateTime.Now; // Actualiza la fecha de actualización

                    _context.SaveChanges();
                    return Ok(usuarioBot);
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
        [Route("ObtenerUsuariobot/{id:int}")]

        public List<catalogoProcesosBotsModel> ObtenerUsuariobot(int id)
        {
            var data = _context.cat_procesos.FromSqlRaw("SELECT * FROM cat_procesos Where Id=" + id).ToList();
            return data;
        }

        [Route("ActualizarBotLimpieza")]
        [HttpPut]
        public dynamic ActualizarBotLimpieza(int id, [FromBody] BotsModellimpieza Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcessLimpieza.FirstOrDefault(b => b.Id == id);
                    var proceso = _context.cat_procesosLimpieza.FirstOrDefault(p => p.Id == Cuenta.ProcesoBotId);

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

        [Route("ActualizarBot")]
        [HttpPut]
        public dynamic ActualizarBot(int id, [FromBody] BotsModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcess.FirstOrDefault(b => b.Id == id);
                    var proceso = _context.cat_procesos.FirstOrDefault(p => p.Id == Cuenta.ProcesoBotId);

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

        [Route("ActualizarBotProcessLimpieza")]
        [HttpPut]
        public dynamic ActualizarBotProcessLimpieza(int id, [FromBody] BotsModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcess.FirstOrDefault(b => b.Id == id);

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

        [Route("ActualizarBotProcess")]
        [HttpPut]
        public dynamic ActualizarBotProcess(int id, [FromBody] BotsModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var bot = _context.BotsProcess.FirstOrDefault(b => b.Id == id);

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

        [Route("EliminarBotLimpieza")]
        [HttpDelete]
        public dynamic EliminarBotLimpieza(int id, [FromBody] BotsModellimpieza Cuenta)
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

        [Route("EliminarBot")]
        [HttpDelete]
        public dynamic EliminarBot(int id, [FromBody] BotsModel Cuenta)
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
        [Route("getCatProcesosLimpieza")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsLimpiezaModel>>> getCatProcesosLimpieza()
        {
            try
            {
                var datos = await _context.cat_procesosLimpieza.Select(proceso => new catalogoProcesosBotsLimpiezaModel
                {
                    Id = proceso.Id,
                    Name_process = proceso.Name_process,
                    Status = proceso.Status
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
        [Route("getCatProcesos")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getCatProcesos()
        {
            try
            {
                var datos = await _context.cat_procesos.Select(proceso => new catalogoProcesosBotsModel
                {
                    Id = proceso.Id,
                    Name_process = proceso.Name_process,
                    Status=proceso.Status
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
        [Route("getProcessOneLimpieza")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsLimpiezaModel>>> getProcessOneLimpieza(int id)
        {
            try
            {
                var query = from proceso in _context.cat_procesosLimpieza
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


        [HttpGet]
        [Route("getProcessOne")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getProcessOne(int id)
        {
            try
            {
                var query = from proceso in _context.cat_procesos
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


        [HttpGet]
        [Route("getAllProcess")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getAllProcess()
        {
            try
            {
                var datos = _context.cat_procesos.FromSqlRaw("select * from cat_procesos;").ToList();
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
        [Route("getOneProceso")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> getOneProceso(int id)
        {
            try
            {
                var query = from proceso in _context.cat_procesos
                            where proceso.Id == id
                            select new
                            {
                                ProcesoName = proceso.Name_process,
                                ProcesoUser = proceso.usuario,
                                ProcesoPassword = proceso.password,
                                ProcesoStatus = proceso.Status,
                                ProcesoID=proceso.Id
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


        [HttpPost]
        [Route("GuardarProceso")]
        public dynamic GuardarProceso(catalogoProcesosBotsModel Cuenta)
        {

            try
            {
                Cuenta.update_At = DateTime.Now;

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("ActualizarProceso")]
        [HttpPut]
        public dynamic ActualizarProceso(int id, [FromBody] catalogoProcesosBotsModel Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    var proceso = _context.cat_procesos.FirstOrDefault(p => p.Id == Cuenta.Id);

                    if (proceso != null)
                    {
                        proceso.Name_process = Cuenta.Name_process;
                        proceso.usuario = Cuenta.usuario;
                        proceso.Status = Cuenta.Status;
                        if (proceso.password != Cuenta.password)
                        {
                            proceso.update_At = DateTime.Now;
                            proceso.password = Cuenta.password; // Actualizar el campo password
                        }

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


        [Route("EliminarProceso")]
        [HttpDelete]
        public dynamic EliminarProceso(int id, [FromBody] catalogoProcesosBotsModel Cuenta)
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
        [Route("getValidationProcesosLimpieza")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsLimpiezaModel>>> getValidationProcesosLimpieza()
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
        [Route("getValidationProcesos")]
        public async Task<ActionResult<IEnumerable< catalogoProcesosBotsModel>>> getValidationProcesos()
        {
            try
            {
                var query = from b in _context.BotsProcess
                            join c in _context.cat_procesos on b.ProcesoBotId equals c.Id
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
        [Route("updateProcessStatusBotLimpieza")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsLimpiezaModel>>> updateProcessStatusBotLimpieza(string ip, string estado)
        {
            try
            {
                var query = from bot in _context.BotsProcessLimpieza
                            join proceso in _context.cat_procesosLimpieza on bot.ProcesoBotId equals proceso.Id
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

        [HttpGet]
        [Route("updateProcessStatusBot")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> updateProcessStatusBot(string ip,string estado)
        {
            try
            {
                var query = from bot in _context.BotsProcess
                            join proceso in _context.cat_procesos on bot.ProcesoBotId equals proceso.Id
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
        [Route("InsertarExcelSeries")]
        [HttpPost]
        public dynamic InsertarExcelSeries([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "INSERT INTO Series (serie) VALUES ('" + data["serie"] + "')";
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
        [Route("getseries")]
        public async Task<ActionResult<IEnumerable<string>>> getseries()
        {
            try
            {
                var datos = _context.Series.FromSqlRaw("exec Sp_getseries").ToList();
                if (datos.Count() > 0)
                {
                    var result = datos.Select(d => d.serie).ToList();

                    return Ok(result);
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
        [Route("ObtenerSeries")]
        public async Task<ActionResult<IEnumerable<series>>> ObtenerSeries()
        {
            try
            {
                var datos = _context.Series.FromSqlRaw("SELECT * FROM Series").ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarFilaserie")]
        [HttpDelete]
        public dynamic EliminarFilaserie(int id, [FromBody] series fila)
        {
            try
            {
                if (id == fila.Id)
                {
                    _context.Remove(fila);
                    _context.SaveChanges();
                    return Ok(fila);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("InsertarExcelSeriesExclucion")]
        [HttpPost]
        public dynamic InsertarExcelSeriesExclucion([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "INSERT INTO seriesExlcucion (serie) VALUES ('" + data["serie"] + "')";
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
        [Route("getseriesExclucion")]
        public async Task<ActionResult<IEnumerable<string>>> getseriesExclucion()
        {
            try
            {
                var datos = _context.seriesExlcucion.FromSqlRaw("exec Sp_getseriesExclucion").ToList();
                if (datos.Count() > 0)
                {
                    var result = datos.Select(d => d.serie).ToList();

                    return Ok(result);
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
        [Route("ObtenerSeriesxclucion")]
        public async Task<ActionResult<IEnumerable<seriesExlcucion>>> ObtenerSeriesxclucion()
        {
            try
            {
                var datos = _context.seriesExlcucion.FromSqlRaw("SELECT * FROM seriesExlcucion").ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarFilaserieExclucion")]
        [HttpDelete]
        public dynamic EliminarFilaserieExclucion(int id, [FromBody] seriesExlcucion fila)
        {
            try
            {
                if (id == fila.Id)
                {
                    _context.Remove(fila);
                    _context.SaveChanges();
                    return Ok(fila);
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
        [Route("getseriesMasivo")]
        public async Task<ActionResult<dynamic>> getseriesMasivo()
        {
            try
            {
                var datos = _context.SeriesMasivo.FromSqlRaw("exec Sp_getseriesMasivo").ToList();
                if (datos.Count() > 0)
                {
                    // Agrupa los valores de 'serie' y 'puntoInventario' en listas separadas.
                    var seriesList = datos.Select(d => d.serie).ToList();
                    var puntoInventarioList = datos.Select(d => d.puntoInventario).ToList();

                    // Crea un objeto anónimo que contiene ambas listas.
                    var result = new
                    {
                        series = seriesList,
                        puntoInventario = puntoInventarioList
                    };

                    return Ok(result);
                }
                else
                {
                    // Si no hay datos, devuelve un objeto con listas que contienen "SIN INFO".
                    var result = new
                    {
                        series = new List<string> { "SIN INFO" },
                        puntoInventario = new List<string> { "SIN INFO" }
                    };

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("InsertarExcelSeriesMasivo")]
        [HttpPost]
        public dynamic InsertarExcelSeriesMasivo([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                foreach (var data in Info)
                {
                    string sql = "INSERT INTO SeriesMasivo (puntoInventario, serie) VALUES (@puntoInventario, @serie)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@puntoInventario", data["PUNTO DE INVENTARIO"].ToString());
                    cmd.Parameters.AddWithValue("@serie", data["Serie"].ToString());
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
        [Route("ObtenerSeriesMasivo")]
        public async Task<ActionResult<IEnumerable<SeriesMasivo>>> ObtenerSeriesMasivo()
        {
            try
            {
                var datos = _context.SeriesMasivo.FromSqlRaw("SELECT * FROM SeriesMasivo").ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //[Route("ActualizaSeries")]
        //[HttpPut]
        //public dynamic ActualizaSeries(int id, [FromBody] series Cuenta)
        //{
        //    try
        //    {
        //        if (id == Cuenta.Id)
        //        {

        //            Cuenta.;
        //            _context.Update(Cuenta);
        //            _context.SaveChanges();
        //            return Ok(Cuenta);

        //        }
        //        else
        //            return NotFound();

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

       

        [HttpPost]
        [Route("guardarformulariohubs")]
        public dynamic guardarformulariohubs(Hobs datos)
        {
            datos.FechaCaptura = DateTime.Now;

            try
            {
                _context.Add(datos);
                _context.SaveChanges();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obtenertablahubs")]
        public async Task<ActionResult<IEnumerable<Hobs>>> Obtenertablahubs(string user)
        {
            try
            {
                var datos = _context.Hobsmodel.FromSqlRaw("SELECT * FROM Hobsmodel").ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarFilabub")]
        [HttpDelete]
        public dynamic EliminarFila(int id, [FromBody] Hobs fila)
        {
            try
            {
                if (id == fila.Id)
                {
                    _context.Remove(fila);
                    _context.SaveChanges();
                    return Ok(fila);
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
        [Route("guardarformulariofallasdepuracion")]
        public dynamic guardarformulariofallasdepuracion(falladepuracion datos)
        {
            datos.FechaCaptura = DateTime.Now;

            try
            {
                _context.Add(datos);
                _context.SaveChanges();
                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obtenertablafallas")]
        public async Task<ActionResult<IEnumerable<falladepuracion>>> Obtenertablafallas(string user)
        {
            try
            {
                var datos = _context.FallasDepuracion.FromSqlRaw("SELECT * FROM FallasDepuracion").ToList();

                return Ok(datos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("EliminarFilafalladepuracion")]
        [HttpDelete]
        public dynamic EliminarFilafalladepuracion(int id, [FromBody] falladepuracion fila)
        {
            try
            {
                if (id == fila.Id)
                {
                    _context.Remove(fila);
                    _context.SaveChanges();
                    return Ok(fila);
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
        [Route("validarContrasenasBotsIzzi")]
        public async Task<ActionResult<IEnumerable<catalogoProcesosBotsModel>>> validarContrasenasBotsIzzi()
        {
            try
            {
                //var datos = _context.cat_procesos.FromSqlRaw("SELECT id,usuario, password, update_at,30 - DATEDIFF(DAY, update_at, GETDATE())  AS dias_pasados_restantes FROM cat_procesos;").ToList();
                var datos = from proceso in _context.cat_procesos.ToList()
                            select new
                            {
                                Id = proceso.Id,
                                Usuario = proceso.usuario,
                                Password = proceso.password,
                                UpdateAt = proceso.update_At,
                                correoUltimodia=proceso.ulitmoDiaSend,
                                DiasPasadosRestantes = 30 - (DateTime.Now.Date - DateTime.Parse(proceso.update_At.ToString()).Date).Days
                            };
                
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
        [Route("EnviarContrasenasBotsIzzi")]
        public async Task<IActionResult> AgregarNuevoCron([FromBody] object Info)
        {
            try
            {
                string apiUrl = "http://192.168.50.33/api/Dac/enviarEmailIzziContrasenas";
                //string requestbody = "{\"key\":\"value\"}"; // cuerpo de la solicitud post en formato json
                //var content = new stringcontent(requestbody, encoding.utf8, "application/json");
                //string requestbody = jsonconvert.serializeobject(info);
                //var content = new stringcontent(requestbody, encoding.utf8, "application/json");
                //var content = new stringcontent(info.tostring(), encoding.utf8, "application/json");
                string jsonString = "" + Info.ToString() + "";
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");


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


        [Route("updateCorreoSend")]
        [HttpGet]
        public dynamic updateCorreoSend(string valor,string id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                string sql = "UPDATE cat_procesos SET ulitmoDiaSend='"+ valor +"' WHERE id='"+ id +"'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }

        }

        [Route("ReprocesarProcesos")]
        [HttpGet]
        public dynamic ReprocesarProcesos()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ActualizarStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

        [Route("ReprocesarProcesosSucursales")]
        [HttpGet]
        public dynamic ReprocesarProcesosSucursales()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("ActualizarStatusSucursales", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

    }
}
