using fiapweb2020.Models;
using fiapweb2020.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.Controllers
{
    public class HomeController: Controller
    {

        public HomeController(INoticiaService noticia)
        {

        }
        //public ActionResult Index()
        //public ViewResult Index()

        public IActionResult Teste()
        {

         
            return View();
        }
        public IActionResult Index()
        {

            ViewData["Nome"] = "Anderson";
            ViewBag.NomeDaAula = "ASP.NET";

            var pessoa = new Pessoa
            {
                Id = 123,
                Nome = "Tonho"
            };

            //return View("Pagina");
            //return View("Pagina",pessoa);
            return View(pessoa);
        }

        [HttpPost]
        public string Index(Pessoa pessoa)
        {

            return "olá";
        }

        public string NuvemDeTags()
        {

            return "olá";
        }
    }
}
