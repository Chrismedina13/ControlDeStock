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
    public class UbicacionProductoController : ControllerBase
    {
        private readonly AppDBContext context;

        public UbicacionProductoController(AppDBContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<UbicacionProducto> Get()
        {
            return context.UbicacionProducto.ToList();
        }

        [HttpPost]
        public ActionResult Post(UbicacionProducto ubicacionProducto)
        {
            UbicacionProductoManager manUbicacionProducto = new UbicacionProductoManager(context);
            string errores = null;
            try
            {
                errores = manUbicacionProducto.Validar(ubicacionProducto);
                if (errores == null)
                {
                    if (manUbicacionProducto.ExisteProductoEnUbicacion(ubicacionProducto.ProductoID,ubicacionProducto.DepositoID,ubicacionProducto.CodUbicacion))
                    {
                        UbicacionProducto ubicacionGuardada = context.UbicacionProducto.First(x => x.DepositoID == ubicacionProducto.DepositoID && x.CodUbicacion == ubicacionProducto.CodUbicacion && x.ProductoID == ubicacionProducto.ProductoID);
                        ubicacionProducto.Cantidad += ubicacionGuardada.Cantidad;
                        context.UbicacionProducto.Remove(ubicacionGuardada);
                    }
                    context.UbicacionProducto.Add(ubicacionProducto);
                    context.SaveChanges();

                    return Ok();
                }
                else
                {
                    return BadRequest(errores);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put(UbicacionProducto ubicacionProducto)
        {
            string errores = null;
            int stockActual = 0;
            UbicacionProductoManager manUbicacionProducto = new UbicacionProductoManager(context);
            try
            {
                errores = manUbicacionProducto.ValidarModficacion(ubicacionProducto);                
                if (errores == null)
                {
                    stockActual = manUbicacionProducto.ObtenerStockProducto(ubicacionProducto.DepositoID, ubicacionProducto.CodUbicacion, ubicacionProducto.ProductoID);
                    if (stockActual - ubicacionProducto.Cantidad == 0) {
                        UbicacionProducto ubicacionGuardada = context.UbicacionProducto.First(x => x.DepositoID == ubicacionProducto.DepositoID && x.CodUbicacion == ubicacionProducto.CodUbicacion && x.ProductoID == ubicacionProducto.ProductoID);
                        context.UbicacionProducto.Remove(ubicacionGuardada);
                    }
                    else {
                        ubicacionProducto.Cantidad = stockActual - ubicacionProducto.Cantidad;
                        context.UbicacionProducto.Update(ubicacionProducto);
                    }
                    context.SaveChangesAsync();
                }
                else
                    return BadRequest(errores);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        //[HttpGet]
        //public HttpGetAttribute() { 

        //}
    }
}
