using ControlDeStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.DTO
{
    public class BusquedaUbicacionProductoDTO
    {
        public BusquedaUbicacionProductoDTO(UbicacionProducto ubicacionProducto)
        {
            this.UbicacionID = ubicacionProducto.CodUbicacion;
            this.Cantidad = ubicacionProducto.Cantidad;
        }

        public string UbicacionID { get; set; }
        public int Cantidad { get; set; }
    }
}
