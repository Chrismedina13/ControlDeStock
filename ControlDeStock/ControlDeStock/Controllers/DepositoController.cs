using ControlDeStock.Context;
using ControlDeStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public ActionResult PutDesposito(string id, Deposito deposito)
        {
            if (id != deposito.DepositoID)
            {
                return BadRequest();
            }

            context.Entry(deposito).State = EntityState.Modified;

            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Deposito.Any(e => e.DepositoID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deposito>> Delete(string id)
        {
            Deposito deposito = await context.Deposito.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            context.Deposito.Remove(deposito);
            await context.SaveChangesAsync();

            return deposito;
        }

    }
}
