using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json.Nodes;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepuracionNotdoneController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;
        public DepuracionNotdoneController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [Route("InsertarBasesDepuracionNotdone")]
        [HttpPost]
        public dynamic InsertarBasesDepuracionNotdone([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            try
            {
                conn.Open();
                foreach (var item in Info)
                {
                    var data = (JsonObject)item;

                    string sql = "INSERT INTO DepuracionNotdone (" +
                                 "CUENTA, NOMBRE_CLIENTE, TIPO_CLIENTE, SUBTIPO_CLIENTE, DIRECCION, " +
                                 "TIPO_ORDEN, SUBTIPO_ORDEN, PAQUETE, NUMERO_ORDEN, ESTADO_ORDEN, " +
                                 "FECHA_APERTURA, FECHA_SOLICITADA, MOTIVO_ORDEN, HUB, RPT, " +
                                 "CIUDAD, PLAZA, VENDEDOR, TECNICO, CREADO_POR, ULTIMA_MOD_POR, " +
                                 "REFERIDO, NUM_REPRO, MOTIVO_REPROGRAMACION, MOTIVO_CANCELACION, " +
                                 "SITUACION_ANTICIPO, PERFIL_PAGO, COMENTARIOS, TEL1, TEL2, TEL3, TEL4) " +
                                 "VALUES (" +
                                 "@CUENTA, @NOMBRE_CLIENTE, @TIPO_CLIENTE, @SUBTIPO_CLIENTE, @DIRECCION, " +
                                 "@TIPO_ORDEN, @SUBTIPO_ORDEN, @PAQUETE, @NUMERO_ORDEN, @ESTADO_ORDEN, " +
                                 "@FECHA_APERTURA, @FECHA_SOLICITADA, @MOTIVO_ORDEN, @HUB, @RPT, " +
                                 "@CIUDAD, @PLAZA, @VENDEDOR, @TECNICO, @CREADO_POR, @ULTIMA_MOD_POR, " +
                                 "@REFERIDO, @NUM_REPRO, @MOTIVO_REPROGRAMACION, @MOTIVO_CANCELACION, " +
                                 "@SITUACION_ANTICIPO, @PERFIL_PAGO, @COMENTARIOS, @TEL1, @TEL2, @TEL3, @TEL4)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CUENTA", data["CUENTA"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NOMBRE_CLIENTE", data["NOMBRE_CLIENTE"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TIPO_CLIENTE", data["TIPO_CLIENTE"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SUBTIPO_CLIENTE", data["SUBTIPO_CLIENTE"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DIRECCION", data["DIRECCION"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TIPO_ORDEN", data["TIPO_ORDEN"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SUBTIPO_ORDEN", data["SUBTIPO_ORDEN"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PAQUETE", data["PAQUETE"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NUMERO_ORDEN", data["NUMERO_ORDEN"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ESTADO_ORDEN", data["ESTADO_ORDEN"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FECHA_APERTURA", data["FECHA_APERTURA"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@FECHA_SOLICITADA", data["FECHA_SOLICITADA"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOTIVO_ORDEN", data["MOTIVO_ORDEN"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@HUB", data["HUB"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RPT", data["RPT"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CIUDAD", data["CIUDAD"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PLAZA", data["PLAZA"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@VENDEDOR", data["VENDEDOR"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TECNICO", data["TECNICO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CREADO_POR", data["CREADO_POR"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ULTIMA_MOD_POR", data["ULTIMA_MOD_POR"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@REFERIDO", data["REFERIDO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NUM_REPRO", data["NUM_REPRO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOTIVO_REPROGRAMACION", data["MOTIVO_REPROGRAMACION"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOTIVO_CANCELACION", data["MOTIVO_CANCELACION"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SITUACION_ANTICIPO", data["SITUACION_ANTICIPO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PERFIL_PAGO", data["PERFIL_PAGO"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMENTARIOS", data["COMENTARIOS"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TEL1", data["TEL1"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TEL2", data["TEL2"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TEL3", data["TEL3"]?.ToString() ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TEL4", data["TEL4"]?.ToString() ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                return Ok(new { message = "Registros insertados correctamente" });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

        [Route("InsertarBasesDepuracionNotDoneOriginal")]
        [HttpPost]
        public IActionResult InsertarBasesDepuracionNotDoneOriginal([FromBody] JsonArray Info)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";

            try
            {
                conn.Open();

                foreach (var item in Info)
                {
                    var data = (JsonObject)item;

                    string sql = @"
                    INSERT INTO DepuracionNotDoneOriginal (CanalDeIngreso, EstadoAdmision, FechaAdmision, TipoDeCuenta, NoTelefonoPrincipal, Telefonos, TipoEMTA, CtaEspecial, Hub, MotivoDeLaOrden, OrdenDePortabilidad, Referido, MotivoDeLaCancelacion, Sistema, SubEstado, NoVTS, ClaveVendedor, MensualidadTotal, TotalDeCNR, DocumentoDePrueba, EstadoEnFecha, CodigoDeTipoDeOrden, Revision, CuentaDeFacturacion, TransferidoAlLibroDeTrabajoDeTransacciones, Equipo, FechaDeLaOrden, Activo, AplicaTablet, ConfirmacionDeInstalacion, NumeroDeOrden, ClaveDelTecnicoPrincipal, Tipo, EstadoDeAsignacionDeCredito, NoProgramaciones, Estado, Compania, Centro, ListaDeImpuestos, Direccion, Apellidos, Nombre, Prioridad, NumeroDeCuenta, Aprobado, AprobadoPor, Moneda, ListaDePrecios, PorcentajeDeDescuento, CompletadaPor, MotivoDeReprogramacion, EnviadaPor, UltimaModificacionPor, UltimaModificacion, Comentarios, CreadoPor, Creado, FechaSolicitada)
                    VALUES (@CanalDeIngreso, @EstadoAdmision, @FechaAdmision, @TipoDeCuenta, @NoTelefonoPrincipal, @Telefonos, @TipoEMTA, @CtaEspecial, @Hub, @MotivoDeLaOrden, @OrdenDePortabilidad, @Referido, @MotivoDeLaCancelacion, @Sistema, @SubEstado, @NoVTS, @ClaveVendedor, @MensualidadTotal, @TotalDeCNR, @DocumentoDePrueba, @EstadoEnFecha, @CodigoDeTipoDeOrden, @Revision, @CuentaDeFacturacion, @TransferidoAlLibroDeTrabajoDeTransacciones, @Equipo, @FechaDeLaOrden, @Activo, @AplicaTablet, @ConfirmacionDeInstalacion, @NumeroDeOrden, @ClaveDelTecnicoPrincipal, @Tipo, @EstadoDeAsignacionDeCredito, @NoProgramaciones, @Estado, @Compania, @Centro, @ListaDeImpuestos, @Direccion, @Apellidos, @Nombre, @Prioridad, @NumeroDeCuenta, @Aprobado, @AprobadoPor, @Moneda, @ListaDePrecios, @PorcentajeDeDescuento, @CompletadaPor, @MotivoDeReprogramacion, @EnviadaPor, @UltimaModificacionPor, @UltimaModificacion, @Comentarios, @CreadoPor, @Creado, @FechaSolicitada);

                ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CanalDeIngreso", data["CanalDeIngreso"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstadoAdmision", data["EstadoAdmision"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaAdmision", data["FechaAdmision"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoDeCuenta", data["TipoDeCuenta"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NoTelefonoPrincipal", data["NoTelefonoPrincipal"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefonos", data["Telefonos"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoEMTA", data["TipoEMTA"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CtaEspecial", data["CtaEspecial"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Hub", data["Hub"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MotivoDeLaOrden", data["MotivoDeLaOrden"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@OrdenDePortabilidad", data["OrdenDePortabilidad"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Referido", data["Referido"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MotivoDeLaCancelacion", data["MotivoDeLaCancelacion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Sistema", data["Sistema"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SubEstado", data["SubEstado"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NoVTS", data["NoVTS"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ClaveVendedor", data["ClaveVendedor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MensualidadTotal", data["MensualidadTotal"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TotalDeCNR", data["TotalDeCNR"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DocumentoDePrueba", data["DocumentoDePrueba"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstadoEnFecha", data["EstadoEnFecha"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodigoDeTipoDeOrden", data["CodigoDeTipoDeOrden"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Revision", data["Revision"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CuentaDeFacturacion", data["CuentaDeFacturacion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TransferidoAlLibroDeTrabajoDeTransacciones", data["TransferidoAlLibroDeTrabajoDeTransacciones"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Equipo", data["Equipo"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaDeLaOrden", data["FechaDeLaOrden"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Activo", data["Activo"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AplicaTablet", data["AplicaTablet"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ConfirmacionDeInstalacion", data["ConfirmacionDeInstalacion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroDeOrden", data["NumeroDeOrden"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ClaveDelTecnicoPrincipal", data["ClaveDelTecnicoPrincipal"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tipo", data["Tipo"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstadoDeAsignacionDeCredito", data["EstadoDeAsignacionDeCredito"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NoProgramaciones", data["NoProgramaciones"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", data["Estado"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Compania", data["Compania"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Centro", data["Centro"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ListaDeImpuestos", data["ListaDeImpuestos"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Direccion", data["Direccion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellidos", data["Apellidos"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Nombre", data["Nombre"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Prioridad", data["Prioridad"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroDeCuenta", data["NumeroDeCuenta"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Aprobado", data["Aprobado"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@AprobadoPor", data["AprobadoPor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Moneda", data["Moneda"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ListaDePrecios", data["ListaDePrecios"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PorcentajeDeDescuento", data["PorcentajeDeDescuento"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CompletadaPor", data["CompletadaPor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MotivoDeReprogramacion", data["MotivoDeReprogramacion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EnviadaPor", data["EnviadaPor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UltimaModificacionPor", data["UltimaModificacionPor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UltimaModificacion", data["UltimaModificacion"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Comentarios", data["Comentarios"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreadoPor", data["CreadoPor"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Creado", data["Creado"]?.ToString() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaSolicitada", data["FechaSolicitada"]?.ToString() ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
                return Ok(new { message = "Registros insertados correctamente" });
            }
            catch (Exception ex)
            {
                conn.Close();
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertarBaseNotDone")]
        public dynamic InsertarBaseNotDone(ArchivosNotdone Cuenta)
        {

            try
            {
                Cuenta.FechaCaptura = DateTime.Now;

                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ActualizarBasesNotDone")]
        [HttpPut]
        public dynamic ActualizarBasesNotDone(int id, [FromBody] ArchivosNotdone Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    Cuenta.FechaCaptura = DateTime.Now;
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
        [Route("getBasesNotDone")]
        public async Task<ActionResult<IEnumerable<ArchivosNotdone>>> getBasesNotDone()
        {
            try
            {
                var datos = _context.ArchivosNotdone.FromSqlRaw("SELECT * FROM ArchivosNotdone ORDER BY FechaCaptura DESC;").ToList();
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

        [Route("BajarExcelFTPArchivosNotDone")]
        [HttpGet]
        public IActionResult BajarExcelFTPArchivosNotDone(string nombre)
        {
            string ftpServerIP = "192.168.50.37";
            string ftpUserName = "rpaback1";
            string ftpPassword = "Cyber123";

            string remoteFilePath = $"/ArchivosNotDone/{nombre}";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpServerIP + remoteFilePath);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                    {
                        responseStream.CopyTo(memoryStream);
                        byte[] fileBytes = memoryStream.ToArray();

                        return File(fileBytes, "application/zip", nombre);
                    }
                }
            }
        }

        [HttpGet]
        [Route("getBasesNotDoneFinal")]
        public async Task<ActionResult<IEnumerable<DepuracionNotdonefinal>>> getBasesNotDoneFinal()
        {
            try
            {
                var datos = _context.DepuracionNotdoneFinal.FromSqlRaw("SELECT * FROM DepuracionNotdoneFinal;").ToList();
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
