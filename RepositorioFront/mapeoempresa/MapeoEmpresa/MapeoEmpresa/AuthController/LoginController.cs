using EntidadesNegocio.InterfazGraficaBlazorDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiciosComponentes.UsuarioServices;
using System.Security.Claims;

namespace MapeoEmpresa.AuthController
{
    public class LoginController : Controller
    {
        private readonly UsuarioService _usuarioService;
        public LoginController(UsuarioService usuarioService)
        {
            _usuarioService= usuarioService;
        }


        [HttpPost("/account")]
        public async Task<IActionResult> Login(UserDTO usuario)
        {
            //if (!ModelState.IsValid) return View();

            int VerificacionUsuario = _usuarioService.UsuarioExiste(usuario.Email);
            if (VerificacionUsuario == 0)
            {
                TempData["Error"] = "El usuario digitado no existe";

                return Redirect("/login/El usuario digitado no existe");
            }


            string hashPasswd = _usuarioService.ObtenerHashPasswd(usuario.Email);

            bool VerificarIdentidad = BCrypt.Net.BCrypt.Verify(usuario.Password, hashPasswd);

            if (VerificarIdentidad)
            {
                var Claims = new List<Claim> {
                    new Claim (ClaimTypes.Name, usuario.Email)

                };

                var roles = _usuarioService.ObtenerRoles(usuario.Email);

                foreach (var rol in roles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, rol));

                }

                

                var identity = new ClaimsIdentity(Claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return Redirect("/index_user");
            }
            else
            {

                TempData["Error"] = "La contraseña es incorrecta. Por favor, inténtelo de nuevo";
            }
            

            return Redirect("/login/Error");
        }

        [HttpPost("/iniciarSesion")]
        public async Task<IActionResult> Login(string usuario, string clave)
        {
            
            int VerificacionUsuario = _usuarioService.UsuarioExiste(usuario);
            if (VerificacionUsuario == 0)
            {
 
                return Redirect("/login/El usuario digitado no existe");
            }


            string hashPasswd = _usuarioService.ObtenerHashPasswd(usuario);

            bool VerificarIdentidad = BCrypt.Net.BCrypt.Verify(clave, hashPasswd);

            if (VerificarIdentidad)
            {
                var Claims = new List<Claim> {
                    new Claim (ClaimTypes.Name, usuario)

                };

                var roles = _usuarioService.ObtenerRoles(usuario);

                foreach (var rol in roles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, rol));

                }

                var politicasAcceso = _usuarioService.ObtenerPoliticasAccesoDeUsuario(usuario);

                foreach (var politica in politicasAcceso)
                {
                    Claims.Add(new Claim(politica,"true"));
                }

                var identity = new ClaimsIdentity(Claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return Redirect("/index_user");
            }
            else
            {

                return Redirect("/login/Clave incorrecta");
            }

        }


        [HttpGet("/cerrarSesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");

            return Redirect("/login");
        }



        [HttpGet("/hola")]
        public  IActionResult Hola()
        {
            


            return Redirect("/");
        }
    }
}
