using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        [HttpGet("Leer")]
        public string LecturaGet(int id) {

            return "Hola";
        }

        [HttpGet("Buscar")]
        public string BusquedaGet(int id)
        {

            return "Chau";
        }
    }
}
