using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("getUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> getUsuarios()
        {
            try
            {
                var datos = _context.Usuarios.FromSqlRaw("select * from Usuarios;").ToList();
                if (datos.Count() > 0)
                {
                    // Enmascarar cada password
                    foreach (var user in datos)
                    {
                        if (!string.IsNullOrEmpty(user.password))
                        {
                            user.password = new string('*', user.password.Length);
                        }
                        else
                        {
                            user.password = "";
                        }
                    }
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

        [Route("ActualizarUsuario")]
        [HttpPut]
        public IActionResult ActualizarUsuario(int id, [FromBody] UsuarioUpdateDto user)
        {
            try
            {
                string editor = user.editor;
                string passwordConfirm = user.passwordConfirm;

                if (id != user.id)
                    return BadRequest("El id no coincide con el usuario.");

                if (string.IsNullOrEmpty(editor))
                    return BadRequest("El campo 'editor' es requerido.");

                var editorUsuario = _context.Usuarios.FirstOrDefault(u => u.correo == editor);
                if (editorUsuario == null)
                    return BadRequest("El correo del editor no existe como usuario válido.");

                // 2. Compara la contraseña recibida con la real del editor
                if (string.IsNullOrEmpty(passwordConfirm) || editorUsuario.password != passwordConfirm)
                {
                    return BadRequest("La contraseña es incorrecta. No se aplicaron cambios.");
                }

                var usuarioOriginal = _context.Usuarios.FirstOrDefault(u => u.id == user.id);
                if (usuarioOriginal == null)
                    return NotFound("Usuario no encontrado.");

                if (!string.IsNullOrEmpty(user.password) && user.password.All(c => c == '*'))
                {
                    user.password = usuarioOriginal.password;
                }

                // 4. Actualiza los datos
                usuarioOriginal.nombre = user.nombre;
                usuarioOriginal.apellido = user.apellido;
                usuarioOriginal.rol = user.rol;
                usuarioOriginal.correo = user.correo;
                usuarioOriginal.usuario = user.usuario;
                usuarioOriginal.password = user.password;
                usuarioOriginal.status = user.status;
                usuarioOriginal.updatedAt = DateTime.Now;
                usuarioOriginal.area = user.area;

                _context.Update(usuarioOriginal);
                _context.SaveChanges();

                var log = new UsuariosLog
                {
                    usuario_id = user.id.ToString(),
                    editor_correo = editor,
                    updatedAt = usuarioOriginal.updatedAt
                };
                _context.UsuariosLog.Add(log);
                _context.SaveChanges();

                return Ok(usuarioOriginal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
