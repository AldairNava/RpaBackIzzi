//using ITfoxtec.Identity.Saml2.Schemas;
//using ITfoxtec.Identity.Saml2;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using Sustainsys.Saml2.AspNetCore2;
//using System.Security.Authentication;
//using System.Security.Claims;
//using WebApplication1.Models;

//namespace WebApplication1.Controllers
//{
//    [Route("[controller]/[action]")]
//    public class AuthController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AuthController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public IActionResult Login()
//        {
//            var props = new AuthenticationProperties
//            {
//                RedirectUri = "/Home/Index" // Cambia esta ruta según sea necesario
//            };

//            return Challenge(props, Saml2Defaults.Scheme);
//        }

//        [HttpPost]
//        public IActionResult Logout()
//        {
//            return SignOut(new AuthenticationProperties
//            {
//                RedirectUri = "/"
//            }, CookieAuthenticationDefaults.AuthenticationScheme, Saml2Defaults.Scheme);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Acs()
//        {
//            var binding = new Saml2PostBinding();
//            var saml2AuthnResponse = new Saml2AuthnResponse();
//            binding.ReadSamlResponse(new AspNetCoreHttpRequest(Request), saml2AuthnResponse);  // Ajuste aquí

//            if (saml2AuthnResponse.Status != Saml2StatusCodes.Success)
//            {
//                throw new AuthenticationException("SAML Response was not successful");
//            }

//            var claimsIdentity = new ClaimsIdentity(saml2AuthnResponse.ClaimsIdentity.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

//            var nombre = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
//            var correo = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
//            var rol = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
//            var otros = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "Otros")?.Value;

//            var usuario = new Usuario
//            {
//                Nombre = nombre,
//                Correo = correo,
//                Rol = rol,
//                Otros = otros
//            };

//            _context.Usuarios.Add(usuario);
//            await _context.SaveChangesAsync();

//            return RedirectToAction("Index", "Home");
//        }
//    }
//}