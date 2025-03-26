using DocumentFormat.OpenXml.Wordprocessing;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("estadisticasEXT")]
        public async Task<ActionResult<IEnumerable<EstadisticasModel>>> estadisticasEXT(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExt '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos1 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExtMotivo '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos2 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExtMes '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos3 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExtDia '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos4 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExtStatus '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos5 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasExtIp '{startDateStr}', '{endDateStr}'").ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivo = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasCC")]
        public async Task<ActionResult<IEnumerable<EstadisticasModel>>> estadisticasCC(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCC '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCMotivo '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCDia '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCTipo '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos6 = await _context.EstadisticasModel.FromSqlRaw($"EXEC EstadisticasCCIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivo = datos1,
                    tipo = datos5,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos6
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasAjustesConValidacion")]
        public async Task<ActionResult<IEnumerable<EstadisticasAjustesConValidacion>>> estadisticasAjustesConValidacion(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacion1 '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionCategoria '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionSolucion '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionDia '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos6 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesConValidacionIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    categoria = datos1,
                    solucion = datos2,
                    mes = datos3,
                    dia = datos4,
                    status = datos5,
                    ip = datos6
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasAjustesSinValidacion")]
        public async Task<ActionResult<IEnumerable<EstadisticasAjustesConValidacion>>> estadisticasAjustesSinValidacion(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacion '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacionMotivoAjuste '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacionMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacionDia '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacionStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasAjustesConValidacion.FromSqlRaw($"EXEC EstadisticasAjustesSinValidacionIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasNotDone")]
        public async Task<ActionResult<IEnumerable<EstadisticasNotDonemodel>>> estadisticasNotDone(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDone '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneMotivo '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneDia '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasNotDoneGeneracionCN")]
        public async Task<ActionResult<IEnumerable<EstadisticasNotDonemodel>>> estadisticasNotDoneGeneracionCN(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCN '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCNMotivo '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCNMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCNDia '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCNStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneGeneracionCNIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasNotDoneCancelacion")]
        public async Task<ActionResult<IEnumerable<EstadisticasNotDonemodel>>> estadisticasNotDoneCancelacion(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotCancelacion '{startDateStr}', '{endDateStr}'")
                                          .ToListAsync();
                var datos1 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneCancelacionMotivo '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos2 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneCancelacionMes '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos3 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotCancelacionDia '{startDateStr}', '{endDateStr}'")
                                           .ToListAsync();
                var datos4 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneCancelacionStatus '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();
                var datos5 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasNotDoneCancelacionIP '{startDateStr}', '{endDateStr}'")
                           .ToListAsync();


                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasCreacionOrdenes")]
        public async Task<ActionResult<IEnumerable<EstadisticasNotDonemodel>>> estadisticasCreacionOrdenes(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenes '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos1 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenesMotivo '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos2 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenesMes '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos3 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenesDia '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos4 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenesStatus '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos5 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCreacionOrdenesIP '{startDateStr}', '{endDateStr}'").ToListAsync();

                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
        [Route("estadisticasCallTrouble")]
        public async Task<ActionResult<IEnumerable<EstadisticasNotDonemodel>>> estadisticasCallTrouble(string startDateStr, string endDateStr)
        {
            try
            {

                ArrayList objs = new ArrayList();

                var datos = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTrouble '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos1 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTroubleMotivo '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos2 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTroubleMes '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos3 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTroubleDia '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos4 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTroubleStatus '{startDateStr}', '{endDateStr}'").ToListAsync();

                var datos5 = await _context.EstadisticasNotDonestats.FromSqlRaw($"EXEC EstadisticasCallTroubleIP '{startDateStr}', '{endDateStr}'").ToListAsync();

                objs.Add(new
                {
                    grafica = datos,
                    motivoAjuste = datos1,
                    mes = datos2,
                    dia = datos3,
                    status = datos4,
                    ip = datos5,
                });

                if (datos.Count() > 0)
                {
                    return Ok(objs);

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
