using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using System.Drawing.Printing;
using WebApplication1.Models;
using System.Drawing;
using Color = System.Drawing.Color;
using Mysqlx.Cursor;
using System.Globalization;
using System;
using System.Text;
using System.Data;

namespace WebApplication1.Controllers
{

    [Route("ReportesIzzi")]
    //[Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ReportesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
            this._configuration = configuration;

        }

        [HttpGet]
        [Route("getTipoReportes")]
        public async Task<ActionResult<IEnumerable<ReportesModel>>> getCuentaReporte()
        {
            try
            {
                var datos = _context.Reportes.FromSqlRaw("SELECT * FROM REPORTES;").ToList();
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
        [Route("getReporteAjustesCasoNegocioCobranza")]
        public IActionResult getReporteAjustesCasoNegocioCobranza(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);
            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 17);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);

            DateTime f1;
            DateTime f2;
            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);

            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("J4", $"Reporte de Ajustes Casos de Negocio Cobranza");
            sl.SetCellValue("J5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("J5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FFBF00";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);
            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);
            sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("J4", "L4");
            sl.MergeWorksheetCells("J5", "L5");


            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Categoria");
            sl.SetCellValue("E9", "Estado");
            sl.SetCellValue("F9", "Fecha de Apertura");
            sl.SetCellValue("G9", "Medios de Contacto");
            sl.SetCellValue("H9", "Motivo del Cliente");
            sl.SetCellValue("I9", "Motivos");
            sl.SetCellValue("J9", "Solución");
            sl.SetCellValue("K9", "SubMotivo");
            sl.SetCellValue("L9", "Fecha Completado");
            sl.SetCellValue("M9", "Fecha de Captura");
            sl.SetCellValue("N9", "Caso de Negocio Generado");
            sl.SetCellValue("O9", "Estatus Caso de Negocio Generado");
            sl.SetCellValue("P9", "Número de Ajuste");
            sl.SetCellValue("Q9", "Estatus");
            sl.SetCellValue("R9", "Procesando");
            sl.SetCellValue("S9", "IP");
            sl.SetCellValue("T9", "Estatus Ajuste");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand("ObtenerReporteAjustesBasesCasosNegocioCobranza", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", f1);  // Pasa la fecha parseada
            cmd.Parameters.AddWithValue("@FechaFin", f2);     // Pasa la fecha parseada
            cmd.CommandTimeout = 480;
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda, reader["categoria"].ToString());
                sl.SetCellValue("E" + celda, reader["estado"].ToString());
                sl.SetCellValue("F" + celda, reader["fechaApertura"].ToString());
                sl.SetCellValue("G" + celda, reader["mediosContacto"].ToString());
                sl.SetCellValue("H" + celda, reader["motivoCliente"].ToString());
                sl.SetCellValue("I" + celda, reader["motivos"].ToString());
                sl.SetCellValue("J" + celda, reader["solucion"].ToString());
                sl.SetCellValue("K" + celda, reader["submotivo"].ToString());
                sl.SetCellValue("L" + celda, reader["fechaCompletado"].ToString());
                sl.SetCellValue("M" + celda, reader["fechaCaptura"].ToString());
                sl.SetCellValue("N" + celda, reader["cnGenerado"].ToString());
                sl.SetCellValue("O" + celda, reader["statusNegocioGenerado"].ToString());
                sl.SetCellValue("P" + celda, reader["numeroAjuste"].ToString());
                sl.SetCellValue("Q" + celda, reader["status"].ToString());
                sl.SetCellValue("R" + celda, reader["procesando"].ToString());
                sl.SetCellValue("S" + celda, reader["IP"].ToString());
                sl.SetCellValue("T" + celda, reader["estatusAjuste"].ToString());
            }

            sl.AddWorksheet("Resumen");//otra pagina
            sl.SelectWorksheet("Resumen");
            sl.SetPageSettings(sp);
            sl.InsertPicture(pic);
            SLPicture pic2 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic2.SetPosition(1, 12);
            pic2.ResizeInPixels(250, 80);
            sl.InsertPicture(pic2);
            sl.SetCellValue("G4", $"Resumen");
            sl.SetCellValue("G5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("G4", estilo);
            //sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("G4", "I4");
            sl.MergeWorksheetCells("G5", "I5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 17);
            sl.SetColumnWidth(7, 15);
            sl.SetColumnWidth(8, 15);
            sl.SetColumnWidth(9, 15);

            sl.SetColumnWidth(11, 17);
            sl.SetColumnWidth(12, 40);
            sl.SetColumnWidth(13, 25);

            sl.SetCellValue("B9", "Pendientes");
            sl.SetCellValue("C9", "Error");
            sl.SetCellValue("D9", "Completadas");
            sl.SetCellValue("E9", "Procesando");
            sl.SetCellValue("F9", "Total");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);

            string sql1 = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' or Status like '%tipo cuenta%' or Status like '%Sin Saldo Vencido%' or Status like '%Excepcion%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN Status='Cerrado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand(sql1, conn1);

            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                sl.SetCellValue("B10", reader1["Pendientes"].ToString());
                sl.SetCellValue("C10", reader1["Error"].ToString());
                sl.SetCellValue("D10", reader1["Completado"].ToString());
                sl.SetCellValue("E10", reader1["Procesando"].ToString());
                sl.SetCellValue("F10", reader1["Total"].ToString());
                sl.SetCellStyle("B10", estilo);
                sl.SetCellStyle("C10", estilo);
                sl.SetCellStyle("D10", estilo);
                sl.SetCellStyle("E10", estilo);
                sl.SetCellStyle("F10", estilo);
            }
            conn1.Close();

            sl.SetCellValue("K9", "Total");
            sl.SetCellValue("L9", "No Pagan/No Respetan Fecha Pactada");
            sl.SetCellValue("M9", "No Procede");
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);

            string sql2 = $"SELECT count(*) as total,SUM(CASE WHEN Status = 'Error Formulario Dinamico No existe' or Status = 'Error Pago Fuera de Fecha' THEN 1 ELSE 0 END) AS NoPaganNoFechaPactada,SUM(CASE WHEN Status = 'Error CN NO Existe' or Status = 'Cuenta no valida' or Status = 'Error SubTipo' or Status = 'Error Sin Saldo Pendiente' or Status = 'Error Estado' or Status = 'Error CN Tipificado Existente' or Status = 'Sin Ajuste Previo' or Status = 'Pago Fuera de Fecha' or Status = 'Error Pago Incorrecto' THEN 1 ELSE 0 END) AS NoProcedio FROM AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn2 = new SqlConnection();
            conn2.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn2.Open();
            SqlCommand cmd2 = new SqlCommand(sql2, conn2);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                sl.SetCellValue("K10", reader2["total"].ToString());
                sl.SetCellValue("L10", reader2["NoPaganNoFechaPactada"].ToString());
                sl.SetCellValue("M10", reader2["NoProcedio"].ToString());
                sl.SetCellStyle("K10", estilo);
                sl.SetCellStyle("L10", estilo);
                sl.SetCellStyle("M10", estilo);
            }
            conn2.Close();


            sl.AddWorksheet("No Completado");//otra pagina
            sl.SelectWorksheet("No Completado");
            sl.SetPageSettings(sp);
            sl.InsertPicture(pic);
            sl.InsertPicture(pic1);

            sl.SetCellValue("J4", $"Reporte RPA no pudo completar debido a que no cumplen alguna de las reglas de negocio");
            sl.SetCellValue("J5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("J5", estilo);

            sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("J4", "L4");
            sl.MergeWorksheetCells("J5", "L5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Categoria");
            sl.SetCellValue("E9", "Estado");
            sl.SetCellValue("F9", "Fecha de Apertura");
            sl.SetCellValue("G9", "Medios de Contacto");
            sl.SetCellValue("H9", "Motivo del Cliente");
            sl.SetCellValue("I9", "Motivos");
            sl.SetCellValue("J9", "Solución");
            sl.SetCellValue("K9", "SubMotivo");
            sl.SetCellValue("L9", "Fecha Completado");
            sl.SetCellValue("M9", "Fecha de Captura");
            sl.SetCellValue("N9", "Caso de Negocio Generado");
            sl.SetCellValue("O9", "Estatus Caso de Negocio Generado");
            sl.SetCellValue("P9", "Número de Ajuste");
            sl.SetCellValue("Q9", "Estatus");
            sl.SetCellValue("R9", "Procesando");
            sl.SetCellValue("S9", "IP");
            sl.SetCellValue("T9", "Estatus Ajuste");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);

            int celda3 = 9;
            string sql3 = $"select * from AjustesBasesCasosNeogcioCobranza where (CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}') and (status='Error Pago Incorrecto' or status='Pago Fuera de Fecha' or status='Sin saldo vencido') order by status;";
            SqlConnection con3 = new SqlConnection();
            con3.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            con3.Open();
            SqlCommand cmd3 = new SqlCommand(sql3, con3);

            SqlDataReader reader3 = cmd3.ExecuteReader();

            while (reader3.Read())
            {
                celda3++;
                sl.SetCellValue("B" + celda3, reader3["cuenta"].ToString());
                sl.SetCellValue("C" + celda3, reader3["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda3, reader3["categoria"].ToString());
                sl.SetCellValue("E" + celda3, reader3["estado"].ToString());
                sl.SetCellValue("F" + celda3, reader3["fechaApertura"].ToString());
                sl.SetCellValue("G" + celda3, reader3["mediosContacto"].ToString());
                sl.SetCellValue("H" + celda3, reader3["motivoCliente"].ToString());
                sl.SetCellValue("I" + celda3, reader3["motivos"].ToString());
                sl.SetCellValue("J" + celda3, reader3["solucion"].ToString());
                sl.SetCellValue("K" + celda3, reader3["submotivo"].ToString());
                sl.SetCellValue("L" + celda3, reader3["fechaCompletado"].ToString());
                sl.SetCellValue("M" + celda3, reader3["fechaCaptura"].ToString());
                sl.SetCellValue("N" + celda3, reader3["cnGenerado"].ToString());
                sl.SetCellValue("O" + celda3, reader3["statusNegocioGenerado"].ToString());
                sl.SetCellValue("P" + celda3, reader3["numeroAjuste"].ToString());
                sl.SetCellValue("Q" + celda3, reader3["status"].ToString());
                sl.SetCellValue("R" + celda3, reader3["procesando"].ToString());
                sl.SetCellValue("S" + celda3, reader3["IP"].ToString());
                sl.SetCellValue("T" + celda3, reader3["estatusAjuste"].ToString());
                //sl.SetCellStyle("B" + celda3, estilo1);
                //sl.SetCellStyle("C" + celda3, estilo1);
                //sl.SetCellStyle("D" + celda3, estilo1);
                //sl.SetCellStyle("E" + celda3, estilo1);
                //sl.SetCellStyle("F" + celda3, estilo1);
                //sl.SetCellStyle("G" + celda3, estilo1);
                //sl.SetCellStyle("H" + celda3, estilo1);
                //sl.SetCellStyle("I" + celda3, estilo1);
                //sl.SetCellStyle("J" + celda3, estilo1);
                //sl.SetCellStyle("K" + celda3, estilo1);
                //sl.SetCellStyle("L" + celda3, estilo1);
                //sl.SetCellStyle("M" + celda3, estilo1);
                //sl.SetCellStyle("N" + celda3, estilo1);
                //sl.SetCellStyle("O" + celda3, estilo1);
                //sl.SetCellStyle("P" + celda3, estilo1);
                //sl.SetCellStyle("Q" + celda3, estilo1);
                //sl.SetCellStyle("R" + celda3, estilo1);
                //sl.SetCellStyle("S" + celda3, estilo1);
                //sl.SetCellStyle("T" + celda3, estilo1);
            }
            con3.Close();







            sl.SelectWorksheet("Detalle");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Ajustes_CN_Cobranza.xlsx"); //regresa el pdf del reporte


        }

        [HttpGet]
        [Route("getReporteAjustesCasoNegocioCobranzaSinValidacion")]
        public IActionResult getReporteAjustesCasoNegocioCobranzaSinValidacion(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 13);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("F4", $"Reporte de Ajustes Casos de Negocio Cobranza Sin Validación");
            sl.SetCellValue("F5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("F5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FFBF00";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("F4", estilo);
            sl.MergeWorksheetCells("F4", "H4");
            sl.MergeWorksheetCells("F5", "H5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 35);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Motivo del Ajuste");
            sl.SetCellValue("D9", "Comentario del Ajuste");
            sl.SetCellValue("E9", "Cantidad a Ajustar");
            sl.SetCellValue("F9", "Tipo de Aplicación");
            sl.SetCellValue("G9", "Usuario");
            sl.SetCellValue("H9", "Fecha Completado");
            sl.SetCellValue("I9", "Fecha Captura");
            sl.SetCellValue("J9", "Estatus");
            sl.SetCellValue("K9", "Procesando");
            sl.SetCellValue("L9", "IP");
            sl.SetCellValue("M9", "Caso de Negocio");
            sl.SetCellValue("N9", "Numero de Ajuste");
            sl.SetCellValue("O9", "Estatus Ajuste");
            sl.SetCellValue("P9", "ID");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand("ObtenerReporteAjustesSinValidacion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", fecha1);
            cmd.Parameters.AddWithValue("@FechaFin", fecha2);
            cmd.CommandTimeout = 480; // 8 minutos
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["motivoAjuste"].ToString());
                sl.SetCellValue("D" + celda, reader["comentarioAjuste"].ToString());
                sl.SetCellValue("E" + celda, reader["cantidadAjustar"].ToString());
                sl.SetCellValue("F" + celda, reader["tipoAplicacion"].ToString());
                sl.SetCellValue("G" + celda, reader["Cve_usuario"].ToString());
                sl.SetCellValue("H" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("I" + celda, reader["FechaCaptura"].ToString());
                sl.SetCellValue("J" + celda, reader["Status"].ToString());
                sl.SetCellValue("K" + celda, reader["Procesando"].ToString());
                sl.SetCellValue("L" + celda, reader["IP"].ToString());
                sl.SetCellValue("M" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("N" + celda, reader["numeroAjuste"].ToString());
                sl.SetCellValue("O" + celda, reader["estatusAjuste"].ToString());
                sl.SetCellValue("P" + celda, reader["Id"].ToString());
                sl.SetCellStyle("B" + celda, estilo1);
                sl.SetCellStyle("C" + celda, estilo1);
                sl.SetCellStyle("D" + celda, estilo1);
                sl.SetCellStyle("E" + celda, estilo1);
                sl.SetCellStyle("F" + celda, estilo1);
                sl.SetCellStyle("G" + celda, estilo1);
                sl.SetCellStyle("H" + celda, estilo1);
                sl.SetCellStyle("I" + celda, estilo1);
                sl.SetCellStyle("J" + celda, estilo1);
                sl.SetCellStyle("K" + celda, estilo1);
                sl.SetCellStyle("L" + celda, estilo1);
                sl.SetCellStyle("M" + celda, estilo1);
                sl.SetCellStyle("N" + celda, estilo1);
                sl.SetCellStyle("O" + celda, estilo1);
            }

            sl.AddWorksheet("Resumen");//otra pagina
            sl.SelectWorksheet("Resumen");
            sl.SetPageSettings(sp);
            sl.InsertPicture(pic);
            SLPicture pic2 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic2.SetPosition(1, 12);
            pic2.ResizeInPixels(250, 80);
            sl.InsertPicture(pic2);
            sl.SetCellValue("G4", $"Resumen");
            sl.SetCellValue("G5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("G4", estilo);
            //sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("G4", "I4");
            sl.MergeWorksheetCells("G5", "I5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 17);
            sl.SetColumnWidth(7, 15);
            sl.SetColumnWidth(8, 15);
            sl.SetColumnWidth(9, 15);

            sl.SetColumnWidth(11, 17);
            sl.SetColumnWidth(12, 40);
            sl.SetColumnWidth(13, 25);

            sl.SetCellValue("B9", "Pendientes");
            sl.SetCellValue("C9", "Error");
            sl.SetCellValue("D9", "Completadas");
            sl.SetCellValue("E9", "Procesando");
            sl.SetCellValue("F9", "Total");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);

            string sql1 = $"SELECT SUM(CASE WHEN Status = 'Pendiente' THEN 1 ELSE 0 END) AS Pendientes,SUM(CASE WHEN Status like '%Error%' THEN 1 ELSE 0 END) AS Error,SUM(CASE WHEN status='Cerrado' or status='Cerrada' or status='Completado' or status='Completada' or status='Caso de Negocio YA Tipificado' THEN 1 ELSE 0 END) AS Completado,SUM(CASE WHEN Status = 'Procesando' THEN 1 ELSE 0 END) AS Procesando,count(*) AS Total FROM AjustesSinValidacion where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand(sql1, conn1);
            cmd.CommandTimeout = 120;
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                sl.SetCellValue("B10", reader1["Pendientes"].ToString());
                sl.SetCellValue("C10", reader1["Error"].ToString());
                sl.SetCellValue("D10", reader1["Completado"].ToString());
                sl.SetCellValue("E10", reader1["Procesando"].ToString());
                sl.SetCellValue("F10", reader1["Total"].ToString());
                sl.SetCellStyle("B10", estilo);
                sl.SetCellStyle("C10", estilo);
                sl.SetCellStyle("D10", estilo);
                sl.SetCellStyle("E10", estilo);
                sl.SetCellStyle("F10", estilo);
            }
            conn1.Close();


            sl.SelectWorksheet("Detalle");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Ajustes_CN_Cobranza_SinValidacion.xlsx"); //regresa el pdf del reporte


        }

        string LimpiarCaracteres(string input)
        {
            // Define los caracteres que quieres permitir en una cadena
            var permitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-:;._ .,;";

            var output = new StringBuilder();
            foreach (var c in input)
            {
                if (permitidos.Contains(c))
                    output.Append(c);
                else
                    output.Append(' '); // Reemplaza los caracteres no permitidos por un espacio en blanco
            }

            return output.ToString();
        }

        [HttpGet]
        [Route("getReporteDepuracionOS")]
        public IActionResult getReporteDepuracionOS(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 14);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("H4", $"Reporte de Bases Canceladas CC");
            sl.SetCellValue("H5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("H5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FF7987";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("H4", estilo);
            sl.MergeWorksheetCells("H4", "J4");
            sl.MergeWorksheetCells("H5", "J5");

            sl.SetColumnWidth(2, 30);
            sl.SetColumnWidth(3, 25);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 55);
            sl.SetColumnWidth(17, 35);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Bases CC");
            sl.SetCellValue("B9", "Estado");
            sl.SetCellValue("C9", "Hub");
            sl.SetCellValue("D9", "Motivo de la Orden");
            sl.SetCellValue("E9", "Nodo");
            sl.SetCellValue("F9", "Cuenta");
            sl.SetCellValue("G9", "Número de Orden");
            sl.SetCellValue("H9", "Tipo");
            sl.SetCellValue("I9", "CN Generado");
            sl.SetCellValue("J9", "Fecha Hora de Cierre");
            sl.SetCellValue("K9", "Fecha Completado");
            sl.SetCellValue("L9", "Fecha de Carga");
            sl.SetCellValue("M9", "Estatus");
            sl.SetCellValue("N9", "Procesando");
            sl.SetCellValue("O9", "Compañia");
            sl.SetCellValue("P9", "Comentarios");
            sl.SetCellValue("Q9", "ID");
            //sl.SetCellValue("R9", "Lead_id");
            //sl.SetCellValue("S9", "Source");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            //sl.SetCellStyle("R9", estilo);
            //sl.SetCellStyle("S9", estilo);

            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select * from DepuracionBasesCanceladasOs where CONVERT(date,FechaCarga) between '{fecha1}' and '{fecha2}';";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["Estado"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Estado"].ToString()));
                sl.SetCellValue("C" + celda, reader["Hub"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Hub"].ToString()));
                sl.SetCellValue("D" + celda, reader["MotivoOrden"] == DBNull.Value ? "" : LimpiarCaracteres(reader["MotivoOrden"].ToString()));
                sl.SetCellValue("E" + celda, reader["Nodo"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Nodo"].ToString()));
                sl.SetCellValue("F" + celda, reader["Cuenta"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Cuenta"].ToString()));
                sl.SetCellValue("G" + celda, reader["NumOrden"] == DBNull.Value ? "" : LimpiarCaracteres(reader["NumOrden"].ToString()));
                sl.SetCellValue("H" + celda, reader["Tipo"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Tipo"].ToString()));
                sl.SetCellValue("I" + celda, reader["Cn_generado"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Cn_generado"].ToString()));
                sl.SetCellValue("J" + celda, reader["FechaHoraCierre"].ToString());
                sl.SetCellValue("K" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("L" + celda, reader["FechaCarga"].ToString());
                sl.SetCellValue("M" + celda, reader["Status"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Status"].ToString()));
                sl.SetCellValue("N" + celda, reader["Procesando"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Procesando"].ToString()));
                sl.SetCellValue("O" + celda, reader["Compania"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Compania"].ToString()));
                sl.SetCellValue("P" + celda, reader["Comentarios"] == DBNull.Value ? "" : LimpiarCaracteres(reader["Comentarios"].ToString()));
                sl.SetCellValue("Q" + celda, reader["Id"].ToString());
                //sl.SetCellValue("R" + celda, reader["lead_id"].ToString());
                //sl.SetCellValue("S" + celda, reader["Source"].ToString());
                //sl.SetCellStyle("B" + celda, estilo1);
                //sl.SetCellStyle("C" + celda, estilo1);
                //sl.SetCellStyle("D" + celda, estilo1);
                //sl.SetCellStyle("E" + celda, estilo1);
                //sl.SetCellStyle("F" + celda, estilo1);
                //sl.SetCellStyle("G" + celda, estilo1);
                //sl.SetCellStyle("H" + celda, estilo1);
                //sl.SetCellStyle("I" + celda, estilo1);
                //sl.SetCellStyle("J" + celda, estilo1);
                //sl.SetCellStyle("K" + celda, estilo1);
                //sl.SetCellStyle("L" + celda, estilo1);
                //sl.SetCellStyle("M" + celda, estilo1);
                //sl.SetCellStyle("N" + celda, estilo1);
                //sl.SetCellStyle("O" + celda, estilo1);
                //sl.SetCellStyle("P" + celda, estilo1);
                //sl.SetCellStyle("Q" + celda, estilo1);
                //sl.SetCellStyle("R" + celda, estilo1);
                //sl.SetCellStyle("S" + celda, estilo1);
            }
            conn.Close();

            sl.AddWorksheet("Bases EXT");//otra pagina
            sl.SelectWorksheet("Bases EXT");

            sl.SetPageSettings(sp);
            sl.InsertPicture(pic);
            SLPicture pic2 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic2.SetPosition(1, 23);
            pic2.ResizeInPixels(250, 80);
            sl.InsertPicture(pic2);
            sl.SetCellValue("L4", $"Reporte de Bases Canceladas EXT");
            sl.SetCellValue("L5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("L4", estilo);
            sl.SetCellStyle("L5", estilo1);
            //sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("L4", "N4");
            sl.MergeWorksheetCells("L5", "N5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 30);
            sl.SetColumnWidth(8, 15);
            sl.SetColumnWidth(9, 15);

            sl.SetColumnWidth(11, 17);
            sl.SetColumnWidth(12, 40);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 30);
            sl.SetColumnWidth(18, 30);
            sl.SetColumnWidth(19, 25);
            sl.SetColumnWidth(20, 25);
            sl.SetColumnWidth(21, 25);
            sl.SetColumnWidth(22, 25);
            sl.SetColumnWidth(23, 25);
            sl.SetColumnWidth(24, 25);
            sl.SetColumnWidth(25, 30);

            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Compañia");
            sl.SetCellValue("D9", "Número de Orden");
            sl.SetCellValue("E9", "Tipo");
            sl.SetCellValue("F9", "Motivo de la Orden");
            sl.SetCellValue("G9", "Fecha Técnico");
            sl.SetCellValue("H9", "Comentarios");
            sl.SetCellValue("I9", "Hub");
            sl.SetCellValue("J9", "RPT");
            sl.SetCellValue("K9", "Region");
            sl.SetCellValue("L9", "Quien Responde");
            sl.SetCellValue("M9", "Transferir");
            sl.SetCellValue("N9", "Nombre Contacto");
            sl.SetCellValue("O9", "Comentarios Cyber");
            sl.SetCellValue("P9", "Nodo");
            sl.SetCellValue("Q9", "Source");
            sl.SetCellValue("R9", "Time Carga");
            sl.SetCellValue("S9", "Estatus");
            sl.SetCellValue("T9", "Fecha Hora Cierre");
            sl.SetCellValue("U9", "CN Generado");
            sl.SetCellValue("V9", "Usuario Creo");
            sl.SetCellValue("W9", "Usuario registro");
            sl.SetCellValue("X9", "Procesando");
            sl.SetCellValue("Y9", "Fecha Completado");
            sl.SetCellValue("Z9", "ID");
            //sl.SetCellValue("AA9", "Lead_id");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);
            sl.SetCellStyle("U9", estilo);
            sl.SetCellStyle("V9", estilo);
            sl.SetCellStyle("W9", estilo);
            sl.SetCellStyle("X9", estilo);
            sl.SetCellStyle("Y9", estilo);
            sl.SetCellStyle("Z9", estilo);

            string sql1 = $"select * from DepuracionBasesCanceladasOsExt where CONVERT(date,time_carga) between '{fecha1}' and '{fecha2}';";
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand(sql1, conn1);
            int celda1 = 9;
            SqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                celda1++;
                sl.SetCellValue("B" + celda1, reader1["Cuenta"].ToString());
                sl.SetCellValue("C" + celda1, reader1["Compania"].ToString());
                sl.SetCellValue("D" + celda1, reader1["NumOrden"].ToString());
                sl.SetCellValue("E" + celda1, reader1["Tipo"].ToString());
                sl.SetCellValue("F" + celda1, reader1["MotivoOrden"].ToString());
                sl.SetCellValue("G" + celda1, reader1["FechaTecnico"].ToString());
                sl.SetCellValue("H" + celda1, reader1["Comentarios"].ToString());
                sl.SetCellValue("I" + celda1, reader1["HUB"].ToString());
                sl.SetCellValue("J" + celda1, reader1["RPT"].ToString());
                sl.SetCellValue("K" + celda1, reader1["Region"].ToString());
                sl.SetCellValue("L" + celda1, reader1["QuienResponde"].ToString());
                sl.SetCellValue("M" + celda1, reader1["Transferir"].ToString());
                sl.SetCellValue("N" + celda1, reader1["NombreContacto"].ToString());
                sl.SetCellValue("O" + celda1, reader1["ComentariosCyber"].ToString());
                sl.SetCellValue("P" + celda1, reader1["Nodo"].ToString());
                sl.SetCellValue("Q" + celda1, reader1["Source"].ToString());
                sl.SetCellValue("R" + celda1, reader1["time_carga"].ToString());
                sl.SetCellValue("S" + celda1, reader1["Status"].ToString());
                sl.SetCellValue("T" + celda1, reader1["FechaHoraCierre"].ToString());
                sl.SetCellValue("U" + celda1, reader1["cn_generado"].ToString());
                sl.SetCellValue("V" + celda1, reader1["usuario_creo"].ToString());
                sl.SetCellValue("W" + celda1, reader1["User_registro"].ToString());
                sl.SetCellValue("X" + celda1, reader1["Procesando"].ToString());
                sl.SetCellValue("Y" + celda1, reader1["FechaCompletado"].ToString());
                sl.SetCellValue("Z" + celda1, reader1["Id"].ToString());
                //sl.SetCellValue("AA" + celda1, reader1["lead_id"].ToString());
                //sl.SetCellStyle("B" + celda1, estilo1);
                //sl.SetCellStyle("C" + celda1, estilo1);
                //sl.SetCellStyle("D" + celda1, estilo1);
                //sl.SetCellStyle("E" + celda1, estilo1);
                //sl.SetCellStyle("F" + celda1, estilo1);
                //sl.SetCellStyle("G" + celda1, estilo1);
                //sl.SetCellStyle("H" + celda1, estilo1);
                //sl.SetCellStyle("I" + celda1, estilo1);
                //sl.SetCellStyle("J" + celda1, estilo1);
                //sl.SetCellStyle("K" + celda1, estilo1);
                //sl.SetCellStyle("L" + celda1, estilo1);
                //sl.SetCellStyle("M" + celda1, estilo1);
                //sl.SetCellStyle("N" + celda1, estilo1);
                //sl.SetCellStyle("O" + celda1, estilo1);
                //sl.SetCellStyle("P" + celda1, estilo1);
                //sl.SetCellStyle("Q" + celda1, estilo1);
                //sl.SetCellStyle("R" + celda1, estilo1);
                //sl.SetCellStyle("S" + celda1, estilo1);
                //sl.SetCellStyle("T" + celda1, estilo1);
                //sl.SetCellStyle("U" + celda1, estilo1);
                //sl.SetCellStyle("V" + celda1, estilo1);
                //sl.SetCellStyle("W" + celda1, estilo1);
                //sl.SetCellStyle("X" + celda1, estilo1);
                //sl.SetCellStyle("Y" + celda1, estilo1);
            }
            conn1.Close();

            sl.SelectWorksheet("Bases CC");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Depuracion_OS.xlsx"); //regresa el pdf del reporte


        }

        [HttpGet]
        [Route("getReporteAjustesNotDone")]
        public IActionResult getReporteAjustesNotDone(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 38);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("T4", $"Reporte de NotDone");
            sl.SetCellValue("T5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("T5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#00B2A9";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("T4", estilo);
            sl.MergeWorksheetCells("T4", "V4");
            sl.MergeWorksheetCells("T5", "V5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 20);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.SetColumnWidth(21, 20);
            sl.SetColumnWidth(22, 25);
            sl.SetColumnWidth(23, 20);
            sl.SetColumnWidth(24, 20);
            sl.SetColumnWidth(25, 20);
            sl.SetColumnWidth(26, 20);
            sl.SetColumnWidth(27, 20);
            sl.SetColumnWidth(28, 20);
            sl.SetColumnWidth(29, 30);
            sl.SetColumnWidth(30, 20);
            sl.SetColumnWidth(31, 20);
            sl.SetColumnWidth(32, 20);
            sl.SetColumnWidth(33, 20);
            sl.SetColumnWidth(34, 20);
            sl.SetColumnWidth(35, 20);
            sl.SetColumnWidth(36, 20);
            sl.SetColumnWidth(37, 30);
            sl.SetColumnWidth(38, 30);
            sl.SetColumnWidth(39, 25);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "NotDone");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Numero de Orden");
            sl.SetCellValue("D9", "Ciudad");
            sl.SetCellValue("E9", "Comentarios");
            sl.SetCellValue("F9", "Creado Por");
            sl.SetCellValue("G9", "Dirección");
            sl.SetCellValue("H9", "Estado de la Orden");
            sl.SetCellValue("I9", "Fecha de Apertura");
            sl.SetCellValue("J9", "Fecha Solicitada");
            sl.SetCellValue("K9", "HUB");
            sl.SetCellValue("L9", "Motivo Cancelación");
            sl.SetCellValue("M9", "Motivo Orden");
            sl.SetCellValue("N9", "Motivo Reprogramación");
            sl.SetCellValue("O9", "Nombre Cliente");
            sl.SetCellValue("P9", "Num Repro.");
            sl.SetCellValue("Q9", "Paquete");
            sl.SetCellValue("R9", "Perfil Pago");
            sl.SetCellValue("S9", "Plaza");
            sl.SetCellValue("T9", "Referido");
            sl.SetCellValue("U9", "RPT");
            sl.SetCellValue("V9", "Situacion Anticipo");
            sl.SetCellValue("W9", "Subtipo Cliente");
            sl.SetCellValue("X9", "Subtipo Orden");
            sl.SetCellValue("Y9", "Técnico");
            sl.SetCellValue("Z9", "Teléfono");
            sl.SetCellValue("AA9", "Tipo Cliente");
            sl.SetCellValue("AB9", "Tipo Orden");
            sl.SetCellValue("AC9", "Ultima Modificacion Por");
            sl.SetCellValue("AD9", "Vendedor");
            sl.SetCellValue("AE9", "Usuario");
            sl.SetCellValue("AF9", "Fecha Completado");
            sl.SetCellValue("AG9", "Fecha Captura");
            sl.SetCellValue("AH9", "Estatus");
            sl.SetCellValue("AI9", "IP");
            sl.SetCellValue("AJ9", "Caso Negocio");
            sl.SetCellValue("AK9", "Estatus Caso Negocio");
            sl.SetCellValue("AL9", "Resultado LLamada");
            sl.SetCellValue("AM9", "Clasificación Orden");
            sl.SetCellValue("AN9", "ID");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);
            sl.SetCellStyle("U9", estilo);
            sl.SetCellStyle("V9", estilo);
            sl.SetCellStyle("W9", estilo);
            sl.SetCellStyle("X9", estilo);
            sl.SetCellStyle("Y9", estilo);
            sl.SetCellStyle("Z9", estilo);
            sl.SetCellStyle("AA9", estilo);
            sl.SetCellStyle("AB9", estilo);
            sl.SetCellStyle("AC9", estilo);
            sl.SetCellStyle("AD9", estilo);
            sl.SetCellStyle("AE9", estilo);
            sl.SetCellStyle("AF9", estilo);
            sl.SetCellStyle("AG9", estilo);
            sl.SetCellStyle("AH9", estilo);
            sl.SetCellStyle("AI9", estilo);
            sl.SetCellStyle("AJ9", estilo);
            sl.SetCellStyle("AK9", estilo);
            sl.SetCellStyle("AL9", estilo);
            sl.SetCellStyle("AM9", estilo);
            sl.SetCellStyle("AN9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select * from EjecucionNotDone where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}';";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 120;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["numeroOrden"].ToString());
                sl.SetCellValue("D" + celda, reader["ciudad"].ToString());
                sl.SetCellValue("E" + celda, reader["comentarios"].ToString());
                sl.SetCellValue("F" + celda, reader["creadoPor"].ToString());
                sl.SetCellValue("G" + celda, reader["direccion"].ToString());
                sl.SetCellValue("H" + celda, reader["estadoOrden"].ToString());
                sl.SetCellValue("I" + celda, reader["fechaApertura"].ToString());
                sl.SetCellValue("J" + celda, reader["fechaSolicitada"].ToString());
                sl.SetCellValue("K" + celda, reader["hub"].ToString());
                sl.SetCellValue("L" + celda, reader["motivoCancelacion"].ToString());
                sl.SetCellValue("M" + celda, reader["motivoOrden"].ToString());
                sl.SetCellValue("N" + celda, reader["motivoReprogramacion"].ToString());
                sl.SetCellValue("O" + celda, reader["nombreCliente"].ToString());
                sl.SetCellValue("P" + celda, reader["numRepro"].ToString());
                sl.SetCellValue("Q" + celda, reader["paquete"].ToString());
                sl.SetCellValue("R" + celda, reader["perfilPago"].ToString());
                sl.SetCellValue("S" + celda, reader["plaza"].ToString());
                sl.SetCellValue("T" + celda, reader["referido"].ToString());
                sl.SetCellValue("U" + celda, reader["rpt"].ToString());
                sl.SetCellValue("V" + celda, reader["situacionAnticipo"].ToString());
                sl.SetCellValue("W" + celda, reader["subtipoCliente"].ToString());
                sl.SetCellValue("X" + celda, reader["subtipoOrden"].ToString());
                sl.SetCellValue("Y" + celda, reader["tecnico"].ToString());
                sl.SetCellValue("Z" + celda, reader["telefono"].ToString());
                sl.SetCellValue("AA" + celda, reader["tipCliente"].ToString());
                sl.SetCellValue("AB" + celda, reader["tipoOrden"].ToString());
                sl.SetCellValue("AC" + celda, reader["UltimaModificacionPor"].ToString());
                sl.SetCellValue("AD" + celda, reader["Vendedor"].ToString());
                sl.SetCellValue("AE" + celda, reader["Cve_usuario"].ToString());
                sl.SetCellValue("AF" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("AG" + celda, reader["FechaCaptura"].ToString());
                sl.SetCellValue("AH" + celda, reader["Status"].ToString());
                sl.SetCellValue("AI" + celda, reader["IP"].ToString());
                sl.SetCellValue("AJ" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("AK" + celda, reader["statusCasoNegocio"].ToString());
                sl.SetCellValue("AL" + celda, reader["resultadoLlamada"].ToString());
                sl.SetCellValue("AM" + celda, reader["clasificacionOrden"].ToString());
                sl.SetCellValue("AN" + celda, reader["Id"].ToString());
            }
            reader.Close();
            conn.Close();
            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_NotDone.xlsx"); //regresa el pdf del reporte
        }

        [HttpGet]
        [Route("getReporteSucursalesError")]
        public IActionResult getReporteSucursalesError(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 7);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            // Combinar celdas de D4 a E4
            sl.MergeWorksheetCells("D4", "F4");
            sl.SetCellValue("D4", $"Reporte de Errores en Ajustes Sucursales");

            // Combinar celdas de D5 a E5
            sl.MergeWorksheetCells("D5", "F5");
            sl.SetCellValue("D5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("D5", estilo1);


            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#00B2A9";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("D4", estilo);
            sl.MergeWorksheetCells("D4", "F4");
            sl.MergeWorksheetCells("D5", "F5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Sucursales");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Status");
            sl.SetCellValue("E9", "Fecha Cargado");
            sl.SetCellValue("F9", "Fecha Completado");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select cuenta, casoNegocio, status, FechaCarga, FechaCompletado from AjustesCambioServicios where convert(date, FechaCarga) between '{fecha1}' and '{fecha2}' and status NOT IN ('Cerrado')";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 120;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda, reader["status"].ToString());
                sl.SetCellValue("E" + celda, reader["FechaCarga"].ToString());
                sl.SetCellValue("F" + celda, reader["FechaCompletado"].ToString());
            }
            reader.Close();
            conn.Close();
            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Sucursales_Error.xlsx"); //regresa el pdf del reporte
        }

        [HttpGet]
        [Route("getReporteAjustesNotDoneSinValidacion")]
        public IActionResult getReporteAjustesNotDoneSinValidacion(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 9);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("E4", $"Reporte de NotDone Cancelación sin Validación");
            sl.SetCellValue("E5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("E5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#00B2A9";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("E4", estilo);
            sl.MergeWorksheetCells("E4", "G4");
            sl.MergeWorksheetCells("E5", "G5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 35);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 20);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.SetColumnWidth(21, 20);
            sl.SetColumnWidth(22, 25);
            sl.SetColumnWidth(23, 20);
            sl.SetColumnWidth(24, 20);
            sl.SetColumnWidth(25, 20);
            sl.SetColumnWidth(26, 20);
            sl.SetColumnWidth(27, 20);
            sl.SetColumnWidth(28, 20);
            sl.SetColumnWidth(29, 30);
            sl.SetColumnWidth(30, 20);
            sl.SetColumnWidth(31, 20);
            sl.SetColumnWidth(32, 20);
            sl.SetColumnWidth(33, 20);
            sl.SetColumnWidth(34, 20);
            sl.SetColumnWidth(35, 20);
            sl.SetColumnWidth(36, 20);
            sl.SetColumnWidth(37, 30);
            sl.SetColumnWidth(38, 30);
            sl.SetColumnWidth(39, 25);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Cancelacion Sin Validacion");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Numero de Orden");
            sl.SetCellValue("D9", "Estado");
            sl.SetCellValue("E9", "Comentarios de la OS");
            sl.SetCellValue("F9", "Usuario");
            sl.SetCellValue("G9", "Fecha Completado");
            sl.SetCellValue("H9", "Fecha Captura");
            sl.SetCellValue("I9", "Estatus");
            sl.SetCellValue("J9", "Procesando");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);

            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select * from CancelacionSinValidacion where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}';";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["numeroOrden"].ToString());
                sl.SetCellValue("D" + celda, reader["estado"].ToString());
                sl.SetCellValue("E" + celda, reader["comentariosOs"].ToString());
                sl.SetCellValue("F" + celda, reader["Cve_usuario"].ToString());
                sl.SetCellValue("G" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("H" + celda, reader["FechaCaptura"].ToString());
                sl.SetCellValue("I" + celda, reader["Status"].ToString());
                sl.SetCellValue("J" + celda, reader["Procesando"].ToString());

                sl.SetCellStyle("B" + celda, estilo1);
                sl.SetCellStyle("C" + celda, estilo1);
                sl.SetCellStyle("D" + celda, estilo1);
                sl.SetCellStyle("E" + celda, estilo1);
                sl.SetCellStyle("F" + celda, estilo1);
                sl.SetCellStyle("G" + celda, estilo1);
                sl.SetCellStyle("H" + celda, estilo1);
                sl.SetCellStyle("I" + celda, estilo1);
                sl.SetCellStyle("J" + celda, estilo1);

            }
            conn.Close();

            sl.AddWorksheet("CN sin Validación");//otra pagina
            sl.SelectWorksheet("CN sin Validación");
            sl.SetPageSettings(sp);
            sl.InsertPicture(pic);
            pic1.SetPosition(1, 12);
            sl.InsertPicture(pic1);
            sl.SetCellValue("F4", $"Reporte de NotDone CN sin Validación");
            sl.SetCellValue("F5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("F5", estilo1);

            sl.SetCellStyle("F4", estilo);
            sl.MergeWorksheetCells("F4", "H4");
            sl.MergeWorksheetCells("F5", "H5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 30);
            sl.SetColumnWidth(5, 30);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 20);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.SetColumnWidth(21, 20);
            sl.SetColumnWidth(22, 25);
            sl.SetColumnWidth(23, 20);
            sl.SetColumnWidth(24, 20);
            sl.SetColumnWidth(25, 20);
            sl.SetColumnWidth(26, 20);
            sl.SetColumnWidth(27, 20);
            sl.SetColumnWidth(28, 20);
            sl.SetColumnWidth(29, 30);
            sl.SetColumnWidth(30, 20);
            sl.SetColumnWidth(31, 20);
            sl.SetColumnWidth(32, 20);
            sl.SetColumnWidth(33, 20);
            sl.SetColumnWidth(34, 20);
            sl.SetColumnWidth(35, 20);
            sl.SetColumnWidth(36, 20);
            sl.SetColumnWidth(37, 30);
            sl.SetColumnWidth(38, 30);
            sl.SetColumnWidth(39, 25);
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Categoria");
            sl.SetCellValue("D9", "Motivos");
            sl.SetCellValue("E9", "Submotivos");
            sl.SetCellValue("F9", "Solución");
            sl.SetCellValue("G9", "Motivo Cliente");
            sl.SetCellValue("H9", "Usuario");
            sl.SetCellValue("I9", "Fecha Completado");
            sl.SetCellValue("J9", "Fecha Captura");
            sl.SetCellValue("K9", "Estatus");
            sl.SetCellValue("L9", "Procesando");
            sl.SetCellValue("M9", "Caso de negocio");
            sl.SetCellValue("N9", "Comentarios");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);

            string sql1 = $"select * from CasosNegocioSinValidacion where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}';";
            SqlConnection conn1 = new SqlConnection();
            conn1.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand(sql1, conn1);
            int celda1 = 9;
            SqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                celda1++;
                sl.SetCellValue("B" + celda1, reader1["cuenta"].ToString());
                sl.SetCellValue("C" + celda1, reader1["categoria"].ToString());
                sl.SetCellValue("D" + celda1, reader1["motivos"].ToString());
                sl.SetCellValue("E" + celda1, reader1["submotivos"].ToString());
                sl.SetCellValue("F" + celda1, reader1["solucion"].ToString());
                sl.SetCellValue("G" + celda1, reader1["motivoCliente"].ToString());
                sl.SetCellValue("H" + celda1, reader1["Cve_usuario"].ToString());
                sl.SetCellValue("I" + celda1, reader1["FechaCompletado"].ToString());
                sl.SetCellValue("J" + celda1, reader1["FechaCaptura"].ToString());
                sl.SetCellValue("K" + celda1, reader1["Status"].ToString());
                sl.SetCellValue("L" + celda1, reader1["Procesando"].ToString());
                sl.SetCellValue("M" + celda1, reader1["casoNegocio"].ToString());
                sl.SetCellValue("N" + celda1, reader1["comentarios"].ToString());
                sl.SetCellStyle("B" + celda1, estilo1);
                sl.SetCellStyle("C" + celda1, estilo1);
                sl.SetCellStyle("D" + celda1, estilo1);
                sl.SetCellStyle("E" + celda1, estilo1);
                sl.SetCellStyle("F" + celda1, estilo1);
                sl.SetCellStyle("G" + celda1, estilo1);
                sl.SetCellStyle("H" + celda1, estilo1);
                sl.SetCellStyle("I" + celda1, estilo1);
                sl.SetCellStyle("J" + celda1, estilo1);
                sl.SetCellStyle("K" + celda1, estilo1);
                sl.SetCellStyle("L" + celda1, estilo1);
                sl.SetCellStyle("M" + celda1, estilo1);
                sl.SetCellStyle("N" + celda1, estilo1);
            }
            conn1.Close();




            sl.SelectWorksheet("Cancelacion Sin Validacion");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_NotDone_SinValidacion.xlsx"); //regresa el pdf del reporte


        }


        [HttpGet]
        [Route("getReporteAjustesCambioServicios")]
        public IActionResult getReporteAjustesCambioServicios(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 13);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("F4", $"Reporte de Ajustes y Cambios de Servicio");
            sl.SetCellValue("F5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("F5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#3B82F6";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("F4", estilo);
            sl.MergeWorksheetCells("F4", "I4");
            sl.MergeWorksheetCells("F5", "I5");

            sl.SetColumnWidth(2, 25);
            sl.SetColumnWidth(3, 30);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 35);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Usuario");
            sl.SetCellValue("E9", "Motivo");
            sl.SetCellValue("F9", "Motivo Cliente");
            sl.SetCellValue("G9", "Estado");
            sl.SetCellValue("H9", "Estatus");
            sl.SetCellValue("I9", "Procesando");
            sl.SetCellValue("J9", "IP");
            sl.SetCellValue("K9", "Fecha de Creación");
            sl.SetCellValue("L9", "Fecha Completado");
            sl.SetCellValue("M9", "Fecha de Carga");
            sl.SetCellValue("N9", "Fecha de Vencimiento");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select * from AjustesCambioServicios where CONVERT(date,FechaCarga) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda, reader["Cve_usuario"].ToString());
                sl.SetCellValue("E" + celda, reader["motivo"].ToString());
                sl.SetCellValue("F" + celda, reader["motivoCliente"].ToString());
                sl.SetCellValue("G" + celda, reader["estado"].ToString());
                sl.SetCellValue("H" + celda, reader["status"].ToString());
                sl.SetCellValue("I" + celda, reader["Procesando"].ToString());
                sl.SetCellValue("J" + celda, reader["IP"].ToString());
                sl.SetCellValue("K" + celda, reader["FechaCreado"].ToString());
                sl.SetCellValue("L" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("M" + celda, reader["FechaCarga"].ToString());
                sl.SetCellValue("N" + celda, reader["FechaVencimiento"].ToString());
                sl.SetCellStyle("B" + celda, estilo1);
                sl.SetCellStyle("C" + celda, estilo1);
                sl.SetCellStyle("D" + celda, estilo1);
                sl.SetCellStyle("E" + celda, estilo1);
                sl.SetCellStyle("F" + celda, estilo1);
                sl.SetCellStyle("G" + celda, estilo1);
                sl.SetCellStyle("H" + celda, estilo1);
                sl.SetCellStyle("I" + celda, estilo1);
                sl.SetCellStyle("J" + celda, estilo1);
                sl.SetCellStyle("K" + celda, estilo1);
                sl.SetCellStyle("L" + celda, estilo1);
                sl.SetCellStyle("M" + celda, estilo1);
                sl.SetCellStyle("N" + celda, estilo1);
            }
            conn.Close();



            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Ajustes_CambioServicio.xlsx"); //regresa el pdf del reporte


        }



        [HttpGet]
        [Route("getReporteCreacionOrden")]
        public IActionResult getReporteCreacionOrden(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 17);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("J4", $"Reporte Creación de Ordenes");
            sl.SetCellValue("J5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("J5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FF671F";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("J4", "L4");
            sl.MergeWorksheetCells("J5", "L5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 30);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Categoria");
            sl.SetCellValue("E9", "Estado");
            sl.SetCellValue("F9", "Fecha de Apertura");
            sl.SetCellValue("G9", "Medios de Contacto");
            sl.SetCellValue("H9", "Motivo del Cliente");
            sl.SetCellValue("I9", "Motivos");
            sl.SetCellValue("J9", "Solución");
            sl.SetCellValue("K9", "SubMotivo");
            sl.SetCellValue("L9", "Fecha Completado");
            sl.SetCellValue("M9", "Fecha de Captura");
            sl.SetCellValue("N9", "Caso de Negocio Generado");
            sl.SetCellValue("O9", "Estatus Caso de Negocio Generado");
            sl.SetCellValue("P9", "Número de Orden");
            sl.SetCellValue("Q9", "Estatus");
            sl.SetCellValue("R9", "Procesando");
            sl.SetCellValue("S9", "IP");
            sl.SetCellValue("T9", "Estatus de la Orden");
            sl.SetCellValue("U9", "ID");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);
            sl.SetCellStyle("U9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select * from NotDoneCreacionOrdenModel where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda, reader["categoria"].ToString());
                sl.SetCellValue("E" + celda, reader["estado"].ToString());
                sl.SetCellValue("F" + celda, reader["fechaApertura"].ToString());
                sl.SetCellValue("G" + celda, reader["mediosContacto"].ToString());
                sl.SetCellValue("H" + celda, reader["motivoCliente"].ToString());
                sl.SetCellValue("I" + celda, reader["motivos"].ToString());
                sl.SetCellValue("J" + celda, reader["solucion"].ToString());
                sl.SetCellValue("K" + celda, reader["submotivo"].ToString());
                sl.SetCellValue("L" + celda, reader["fechaCompletado"].ToString());
                sl.SetCellValue("M" + celda, reader["fechaCaptura"].ToString());
                sl.SetCellValue("N" + celda, reader["cnGenerado"].ToString());
                sl.SetCellValue("O" + celda, reader["statusNegocioGenerado"].ToString());
                sl.SetCellValue("P" + celda, reader["numeroOrden"].ToString());
                sl.SetCellValue("Q" + celda, reader["status"].ToString());
                sl.SetCellValue("R" + celda, reader["procesando"].ToString());
                sl.SetCellValue("S" + celda, reader["IP"].ToString());
                sl.SetCellValue("T" + celda, reader["estatusOrden"].ToString());
                sl.SetCellValue("U" + celda, reader["Id"].ToString());
            }

            sl.SelectWorksheet("Detalle");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Creacion_OS.xlsx"); //regresa el pdf del reporte


        }

        [HttpGet]
        [Route("ReporteAjustesCasoNegocioCobranza")]
        public IActionResult ReporteAjustesCasoNegocioCobranza(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 17);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("J4", $"Reporte de Ajustes Casos de Negocio Cobranza");
            sl.SetCellValue("J5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("J5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FFBF00";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("J4", "L4");
            sl.MergeWorksheetCells("J5", "L5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Caso de Negocio");
            sl.SetCellValue("D9", "Categoria");
            sl.SetCellValue("E9", "Estado");
            sl.SetCellValue("F9", "Fecha de Apertura");
            sl.SetCellValue("G9", "Medios de Contacto");
            sl.SetCellValue("H9", "Motivo del Cliente");
            sl.SetCellValue("I9", "Motivos");
            sl.SetCellValue("J9", "Solución");
            sl.SetCellValue("K9", "SubMotivo");
            sl.SetCellValue("L9", "Fecha Completado");
            sl.SetCellValue("M9", "Fecha de Captura");
            sl.SetCellValue("N9", "Caso de Negocio Generado");
            sl.SetCellValue("O9", "Estatus Caso de Negocio Generado");
            sl.SetCellValue("P9", "Número de Ajuste");
            sl.SetCellValue("Q9", "Estatus");
            sl.SetCellValue("R9", "Procesando");
            sl.SetCellValue("S9", "IP");
            sl.SetCellValue("T9", "Estatus Ajuste");
            sl.SetCellValue("U9", "ID");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            sl.SetCellStyle("M9", estilo);
            sl.SetCellStyle("N9", estilo);
            sl.SetCellStyle("O9", estilo);
            sl.SetCellStyle("P9", estilo);
            sl.SetCellStyle("Q9", estilo);
            sl.SetCellStyle("R9", estilo);
            sl.SetCellStyle("S9", estilo);
            sl.SetCellStyle("T9", estilo);
            sl.SetCellStyle("U9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            //string sql = $"select * from AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            string sql = $"select cuenta,casoNegocio,categoria,estado,fechaApertura,Id,mediosContacto,motivoCliente,motivos,solucion,submotivo,fechaCompletado,FORMAT(FechaCaptura, 'dd/MM/yyyy hh:mm:ss tt') AS fechaCaptura,cnGenerado,statusNegocioGenerado,numeroAjuste,status,procesando,ip,estatusAjuste from AjustesBasesCasosNeogcioCobranza where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 120;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["casoNegocio"].ToString());
                sl.SetCellValue("D" + celda, reader["categoria"].ToString());
                sl.SetCellValue("E" + celda, reader["estado"].ToString());
                sl.SetCellValue("F" + celda, reader["fechaApertura"].ToString());
                sl.SetCellValue("G" + celda, reader["mediosContacto"].ToString());
                sl.SetCellValue("H" + celda, reader["motivoCliente"].ToString());
                sl.SetCellValue("I" + celda, reader["motivos"].ToString());
                sl.SetCellValue("J" + celda, reader["solucion"].ToString());
                sl.SetCellValue("K" + celda, reader["submotivo"].ToString());
                sl.SetCellValue("L" + celda, reader["fechaCompletado"].ToString());
                sl.SetCellValue("M" + celda, reader["fechaCaptura"].ToString());
                sl.SetCellValue("N" + celda, reader["cnGenerado"].ToString());
                sl.SetCellValue("O" + celda, reader["statusNegocioGenerado"].ToString());
                sl.SetCellValue("P" + celda, reader["numeroAjuste"].ToString());
                sl.SetCellValue("Q" + celda, reader["status"].ToString());
                sl.SetCellValue("R" + celda, reader["procesando"].ToString());
                sl.SetCellValue("S" + celda, reader["IP"].ToString());
                sl.SetCellValue("T" + celda, reader["estatusAjuste"].ToString());
                sl.SetCellValue("U" + celda, reader["Id"].ToString());
            }

            sl.SelectWorksheet("Detalle");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Ajustes_CN_Cobranza.xlsx"); //regresa el pdf del reporte


        }

        [HttpGet]
        [Route("ReporteOrdenesCallTrouble")]
        public IActionResult ReporteOrdenesCallTrouble(string fecha1, string fecha2)
        {
            SLDocument sl = new SLDocument();
            var pathimg = $"{this._webHostEnvironment.WebRootPath}\\img\\img.png";
            var pathimg2 = $"{this._webHostEnvironment.WebRootPath}\\img\\Logo_Izzi.svg.png";
            SLStyle estilo1 = sl.CreateStyle();
            estilo1.Alignment.Horizontal = HorizontalAlignmentValues.Center;


            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(pathimg);
            byte[] ba;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Close();
                ba = ms.ToArray();
            }
            SLPicture pic = new SLPicture(ba, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic.SetPosition(1, 1);
            pic.ResizeInPixels(250, 80);
            sl.InsertPicture(pic);

            System.Drawing.Bitmap bm1 = new System.Drawing.Bitmap(pathimg2);
            byte[] ba1;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                bm1.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                ms1.Close();
                ba1 = ms1.ToArray();
            }
            SLPicture pic1 = new SLPicture(ba1, DocumentFormat.OpenXml.Packaging.ImagePartType.Png);
            pic1.SetPosition(1, 17);
            pic1.ResizeInPixels(250, 80);
            sl.InsertPicture(pic1);
            DateTime f1;
            DateTime f2;

            DateTime.TryParseExact(fecha1, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f1);
            string fechaFormateada = f1.ToString("dd-MM-yyyy");
            DateTime.TryParseExact(fecha2, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out f2);
            string fechaFormateada1 = f2.ToString("dd-MM-yyyy");

            sl.SetCellValue("J4", $"Reporte de Ajustes Casos de Negocio Cobranza");
            sl.SetCellValue("J5", $"Fecha de Inicio: {fechaFormateada} A Fecha Final: {fechaFormateada1}");
            sl.SetCellStyle("J5", estilo1);

            SLStyle estilo = sl.CreateStyle();
            estilo.Font.FontName = "Arial";
            estilo.Font.FontSize = 12;
            estilo.Font.Bold = true;
            string colorHex = "#FFBF00";
            Color customColor = ColorTranslator.FromHtml(colorHex);
            estilo.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            estilo.Font.SetFontThemeColor(SLThemeColorIndexValues.Light1Color);

            estilo.Fill.SetPattern(PatternValues.Solid, customColor, SLThemeColorIndexValues.Accent1Color);

            sl.SetCellStyle("J4", estilo);
            sl.MergeWorksheetCells("J4", "L4");
            sl.MergeWorksheetCells("J5", "L5");

            sl.SetColumnWidth(2, 20);
            sl.SetColumnWidth(3, 20);
            sl.SetColumnWidth(4, 20);
            sl.SetColumnWidth(5, 20);
            sl.SetColumnWidth(6, 30);
            sl.SetColumnWidth(7, 25);
            sl.SetColumnWidth(8, 30);
            sl.SetColumnWidth(9, 25);
            sl.SetColumnWidth(10, 30);
            sl.SetColumnWidth(11, 30);
            sl.SetColumnWidth(12, 25);
            sl.SetColumnWidth(13, 25);
            sl.SetColumnWidth(14, 30);
            sl.SetColumnWidth(15, 40);
            sl.SetColumnWidth(16, 20);
            sl.SetColumnWidth(17, 20);
            sl.SetColumnWidth(18, 20);
            sl.SetColumnWidth(19, 20);
            sl.SetColumnWidth(20, 20);
            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Detalle");
            sl.SetCellValue("B9", "Cuenta");
            sl.SetCellValue("C9", "Tipo");
            sl.SetCellValue("D9", "Motivo");
            sl.SetCellValue("E9", "Comentarios");
            sl.SetCellValue("F9", "Numero de Orden");
            sl.SetCellValue("G9", "Numero de CN");
            sl.SetCellValue("H9", "Status");
            sl.SetCellValue("I9", "Bot");
            sl.SetCellValue("J9", "Fecha Completado");
            sl.SetCellValue("K9", "Fecha Capturado");
            sl.SetCellValue("L9", "Usuario");
            sl.SetCellStyle("B9", estilo);
            sl.SetCellStyle("C9", estilo);
            sl.SetCellStyle("D9", estilo);
            sl.SetCellStyle("E9", estilo);
            sl.SetCellStyle("F9", estilo);
            sl.SetCellStyle("G9", estilo);
            sl.SetCellStyle("H9", estilo);
            sl.SetCellStyle("I9", estilo);
            sl.SetCellStyle("J9", estilo);
            sl.SetCellStyle("K9", estilo);
            sl.SetCellStyle("L9", estilo);
            SLPageSettings sp = new SLPageSettings
            {
                ShowGridLines = false
            };
            sl.SetPageSettings(sp);

            int celda = 9;
            string sql = $"select Cuenta,Tipo,Motivo,Comentarios,NumeroCN,Status,Cve_usuario,Ip,NumeroOrden,FechaCaptura,FechaCompletado from OrdenTroubleCall where CONVERT(date,FechaCaptura) between '{fecha1}' and '{fecha2}'";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:rpawinserver.database.windows.net,1433;Initial Catalog=WinDBRPA;Persist Security Info=False;User ID=RpaWinDB;Password=Ruka0763feTrfg;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=120;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 120;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                celda++;
                sl.SetCellValue("B" + celda, reader["Cuenta"].ToString());
                sl.SetCellValue("C" + celda, reader["Tipo"].ToString());
                sl.SetCellValue("D" + celda, reader["Motivo"].ToString());
                sl.SetCellValue("E" + celda, reader["Comentarios"].ToString());
                sl.SetCellValue("F" + celda, reader["NumeroOrden"].ToString());
                sl.SetCellValue("G" + celda, reader["NumeroCN"].ToString());
                sl.SetCellValue("H" + celda, reader["Status"].ToString());
                sl.SetCellValue("I" + celda, reader["Ip"].ToString());
                sl.SetCellValue("J" + celda, reader["FechaCompletado"].ToString());
                sl.SetCellValue("K" + celda, reader["FechaCaptura"].ToString());
                sl.SetCellValue("L" + celda, reader["Cve_usuario"].ToString());
            }

            sl.SelectWorksheet("Detalle");


            MemoryStream stream = new MemoryStream();
            sl.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/zip", "Reporte_Ordenes_Call_Trouble.xlsx"); //regresa el pdf del reporte


        }

    }
}
