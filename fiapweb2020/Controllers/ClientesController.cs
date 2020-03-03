using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fiapweb2020.Models;
using Microsoft.AspNetCore.Mvc;

namespace fiapweb2020.Controllers
{
    public class ClientesController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {


            return View("CreateClientes");
        }
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {


            return View("CreateClientes");

        }
    }
}