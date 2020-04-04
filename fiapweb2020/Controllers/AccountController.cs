using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace fiapweb2020.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Clientes");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ViewModels.LoginViewModel model)
        {
            //acessar um db e validar usuario e senha
            if (model.UserName == "rodolfo" && model.Password == "123123")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
                //claims.Add(new Claim(ClaimTypes., model.UserName));


                var id = new ClaimsIdentity(claims, "password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.SignInAsync("app", principal, new AuthenticationProperties() { IsPersistent = model.IsPersistent });;

                return RedirectToAction("Index", "Clientes");
            }

            return View();
        }

    
        public async Task<IActionResult> Logoff()
        {
            //await HttpContext.SignOutAsync("app");
            await HttpContext.SignOutAsync("app");

            return RedirectToAction("Index","Home");
        }



        [HttpGet]
        public IActionResult Denied()
        {


            return View();
        }
    }
}