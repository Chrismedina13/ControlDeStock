using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Models
{
    public class UbicacionProducto
    {
        public string DepositoID { get; set; }
        public string CodUbicacion { get; set; }
        public string ProductoID { get; set; }
        public int Cantidad { get; set; }


    }
}
