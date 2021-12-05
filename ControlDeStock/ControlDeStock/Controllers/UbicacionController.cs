using ControlDeStock.Context;
using ControlDeStock.Managers;
using ControlDeStock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly AppDBContext context;

        public UbicacionController(AppDBContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Ubicacion> Get()
        {
            return context.Ubicacion.ToList();
        }

        [HttpPost]
        public ActionResult Post(Ubicacion ubicacion)
        {
            UbicacionManager manUbicacion = new UbicacionManager(context);
            string errores = null;
            try
            {   
                errores = manUbicacion.Validar(ubicacion);
                if (errores == null)
                {
                    context.Ubicacion.Add(ubicacion);
                    context.SaveChanges();
                    return Ok();
                }
                else {
                    return BadRequest(errores);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{depositoID}/{ubicacionID}")]
        public ActionResult PutUbicacion(string depositoID, string ubicacionID, Ubicacion ubicacion)
        {
            if (depositoID != ubicacion.DepositoID || ubicacionID != ubicacion.CodUbicacion)
            {
                return BadRequest();
            }

            context.Entry(ubicacion).State = EntityState.Modified;

            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Ubicacion.Any(e => e.CodUbicacion == ubicacionID && e.DepositoID == depositoID))
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

        [HttpDelete("{depositoID}/{ubicacionID}")]
        public ActionResult<Ubicacion> Delete(string depositoID, string ubicacionID)
        {
            Ubicacion ubicacion =  context.Ubicacion.First(x => x.DepositoID == depositoID && x.CodUbicacion == ubicacionID);
            if (ubicacion == null)
            {
                return NotFound();
            }

            context.Ubicacion.Remove(ubicacion);
            context.SaveChangesAsync();

            return ubicacion;
        }

    }
}
