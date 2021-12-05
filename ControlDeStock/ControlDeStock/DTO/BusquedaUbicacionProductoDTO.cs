using ControlDeStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.DTO
{
    public class BusquedaUbicacionProductoDTO
    {
        public BusquedaUbicacionProductoDTO(string codUbicacion, int cantidad)
        {
            this.UbicacionID = codUbicacion;
            this.Cantidad = cantidad;
        }

        public string UbicacionID { get; set; }
        public int Cantidad { get; set; }
    }
}
