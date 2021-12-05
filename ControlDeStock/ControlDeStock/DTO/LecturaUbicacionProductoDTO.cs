using ControlDeStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.DTO
{
    public class LecturaUbicacionProductoDTO
    {
        public LecturaUbicacionProductoDTO(UbicacionProducto ubicacionProducto)
        {
            this.ProductoID = ubicacionProducto.ProductoID;
            this.Cantidad = ubicacionProducto.Cantidad;
        }

        public string ProductoID { get; set; }
        public int Cantidad { get; set; }
    }
}
