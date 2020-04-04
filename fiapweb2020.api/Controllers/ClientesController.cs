using fiapweb2020.api.Custom;
using fiapweb2020.core.Contexts;
using fiapweb2020.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.api.Controllers
{
    //[CustomAuthorize]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("default")]
    public class ClientesController : ControllerBase
    {
        private ClienteContext _context;

        public ClientesController(ClienteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> Get()
        {
            //return NotFound();

            //return Ok(_context.Clientes.ToList());

            return _context.Clientes.ToList();
        }
        #region exemplos de result
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(List<Cliente>))]
        //[ProducesResponseType(404)]
        //public IActionResult Get()
        //{
        //    //return NotFound();

        //    return Ok( _context.Clientes.ToList());
        //}


        //[HttpGet]
        //public List<Cliente> Get()
        //{
        //    //return NotFound();

        //    return _context.Clientes.ToList();
        //}
        #endregion

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _context.Clientes.Where(a => a.Id == id).FirstOrDefault();
            if (cliente == null)
                return NotFound();

            return Ok(cliente);

            //return _context.Clientes.Where(a=>a.Id == id).First();
            //return _context.Clientes.Where(a=>a.Id == id).Single();
            //return _context.Clientes.Where(a=>a.Id == id).SingleOrDefault();
        }

        [HttpPost]
        public ActionResult<Cliente> Post(Cliente cliente)
        {
            //Sem [ApiController]
            //if (ModelState.IsValid)
            //{
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Created($"/api/clientes/{cliente.Id}", cliente);
            //}
            //return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Cliente> Put(int id, Cliente cliente)
        {
            _context.Attach(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(cliente);
        }



        [HttpPut]
        [Route("{id}/star")]
        public ActionResult<Cliente> Star(int id)
        {
            //_context.Attach(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            return NoContent();
        }


        [HttpDelete]
        [Route("{id}/star")]
        public ActionResult<Cliente> RemoveStar(int id)
        {
            //_context.Attach(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_context.SaveChanges();

            return NoContent();
        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(a => a.Id == id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return NoContent();
        }



    }
}
