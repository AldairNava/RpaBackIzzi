using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            var htmlContent = @"
            <!DOCTYPE html>
            <html lang='es'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>RPABackIzzi en funcionamiento</title>
                <style>
                    body {
                        margin: 0;
                        padding: 0;
                        background: url('https://opensistemas.com/wp-content/uploads/2021/02/Microsoft-Datacenter-Quincy-1.jpg') no-repeat center center fixed;
                        background-size: cover;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        text-align: center;
                        color: white;
                    }
                    .content {
                        text-align: center;
                    }
                    h1 {
                        font-size: 6rem; /* Tamaño de fuente doblado */
                        text-shadow: 6px 6px 12px #000000; /* Contorno más ancho */
                    }
                </style>
            </head>
            <body>
                <div class='content'>
                    <h1>RPABackIzzi en funcionamiento</h1>
                </div>
            </body>
            </html>";
            return Content(htmlContent, "text/html");
        }
    }
}
