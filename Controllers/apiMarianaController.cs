using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiMarianaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IScheduler _scheduler;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public apiMarianaController(ApplicationDbContext context, IScheduler scheduler, IConfiguration configuration)
        {
            _context = context;
            _scheduler = scheduler;
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpPost]
        [Route("generarRegistros")]
        public async Task<IActionResult> generarRegistros([FromBody] CronInfo cronInfo)
        {
            try
            {
                string iniString = cronInfo.ini.ToString("yyyy-MM-dd");
                string finString = cronInfo.fin.ToString("yyyy-MM-dd");

                CronInfo cronInfoFormatted = new CronInfo
                {
                    ini = DateTime.ParseExact(iniString, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    fin = DateTime.ParseExact(finString, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };

                Console.WriteLine("Datos a enviar:");
                Console.WriteLine($"Ini: {iniString}");
                Console.WriteLine($"Fin: {finString}");

                string apiUrl = "http://192.168.51.210:1023/pruebaController/countAudios";

                string jsonString = JsonConvert.SerializeObject(cronInfoFormatted);

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                _httpClient.Timeout = TimeSpan.FromSeconds(600);

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

        [HttpPost]
        [Route("construyeMetodo")]
        public async Task<IActionResult> ConstruyeMetodo([FromBody] JsonElement jsonData)
        {
            try
            {
                string controlador = jsonData.GetProperty("controlador").GetString();
                string metodo = jsonData.GetProperty("metodo").GetString();

                //string url = $"http://192.168.51.210/api/{controlador}/{metodo}";
                string url = $"http://192.168.51.210:1023/{controlador}/{metodo}";

                StringContent content = new StringContent(jsonData.ToString(), System.Text.Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(600);

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (response.Content.Headers.ContentType.MediaType == "application/json")
                        {
                            return Content(responseBody, "application/json");
                        }
                        else
                        {
                            return StatusCode(500, "Formato de respuesta incorrecta");
                        }
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error en la petición: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error procesando el JSON: " + ex.Message);
            }
        }




        public class CronInfo
        {
            public DateTime ini { get; set; }
            public DateTime fin { get; set; }
        }

        [HttpPost]
        [Route("copiaAudio")]
        public async Task<IActionResult> CopiaAudio([FromBody] JsonElement jsonData)
        {
            try
            {
                // Extraer el nombre del archivo del JSON recibido
                string audioFileName = jsonData.GetProperty("filename").GetString();

                // Configuración del FTP
                string ftpUrl = $"ftp://192.168.50.37/Audios/{audioFileName}"; // URL del archivo en el servidor FTP
                string ftpUsername = "rpaback1";
                string ftpPassword = "Cyber123";

                // Crear la solicitud FTP para descargar el archivo
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Obtener la respuesta del servidor FTP
                using FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
                using Stream responseStream = response.GetResponseStream();

                // Definir la ruta donde se guardará el archivo (Audios en la raíz de la soulúción)
                string localFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Audios", audioFileName);

                // Crear la carpeta si no existe
                Directory.CreateDirectory(Path.GetDirectoryName(localFilePath));

                // Guardar el archivo localmente
                using FileStream fileStream = new FileStream(localFilePath, FileMode.Create);
                await responseStream.CopyToAsync(fileStream);

                // Retornar el estado de la conexión
                return Ok($"Archivo '{audioFileName}' copiado exitosamente a: {localFilePath}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error conectando al FTP o copiando el archivo: " + ex.Message);
            }
        }

    }

}
