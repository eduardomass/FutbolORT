using Futbol.BaseDatos;
using Futbol.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Futbol.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly FutbolDbContext _context;
        private const string _Return_Url = "ReturnUrl";

        public LoginController(FutbolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            // Guardamos la url de retorno para que una vez concluído el login del 
            // usuario lo podamos redirigir a la página en la que se encontraba antes
            TempData[_Return_Url] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, Rol rol)
        {
            string returnUrl = TempData[_Return_Url] as string;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                Usuario usuario = null;

                if (rol == Rol.Jugador)
                {
                    usuario = _context.Jugadores.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Password);
                }
                else if (rol == Rol.Administrador)
                {
                    usuario = _context.Administradores.FirstOrDefault(o => o.Email.ToUpper() == email.ToUpper() && password == o.Password);
                }

                if (usuario != null)
                {
                    // Se crean las credenciales del usuario que serán incorporadas al contexto
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    // El lo que luego obtendré al acceder a User.Identity.Name
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                    // Se utilizará para la autorización por roles
                    identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.ToString()));

                    // Lo utilizaremos para acceder al Id del usuario que se encuentra en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    // Lo utilizaremos cuando querramos mostrar el nombre del usuario logueado en el sistema.
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nombre));

                    //identity.AddClaim(new Claim(nameof(Usuario.Foto), usuario.Foto ?? string.Empty));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    // En este paso se hace el login del usuario al sistema
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    TempData["LoggedIn"] = true;

                    if (!string.IsNullOrWhiteSpace(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            // Completo estos dos campos para poder retornar a la vista en caso de errores.
            ViewBag.Error = "Usuario o contraseña incorrectos";
            ViewBag.Email = email;
            TempData[_Return_Url] = returnUrl;

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult NoAutorizado()
        {
            return View();
        }


    }
}
