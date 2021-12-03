using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Models
{
    public class Ubicacion
    {
        public string DepositoID { get; set; }
        public string CodUbicacion { get; set; }
        public string Area { get; set; }
        public string Pasillo { get; set; }
        public string Fila { get; set; }
        public string Cara { get; set; }

    }
}
