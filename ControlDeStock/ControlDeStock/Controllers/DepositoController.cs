using ControlDeStock.Context;
using ControlDeStock.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlDeStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly AppDBContext context;

        public DepositoController(AppDBContext context)
        {
            this.context = context;
        }

        // GET: api/<DepositoController>
        [HttpGet]
        public IEnumerable<Deposito> Get()
        {
            return context.Deposito.ToList();
        }

        // POST api/<DepositoController>
        [HttpPost]
        public ActionResult Post(Deposito deposito)
        {
            try
            {
                context.Deposito.Add(deposito);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex) {

                return BadRequest();
            }
            
        }

    }
}
