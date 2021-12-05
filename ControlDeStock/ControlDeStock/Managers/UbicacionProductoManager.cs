using ControlDeStock.Context;
using ControlDeStock.Models;
using ControlDeStock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Managers
{
    public class UbicacionProductoManager
    {
        private AppDBContext context;

        public UbicacionProductoManager(AppDBContext context)
        {
            this.context = context;
        }

        public string Validar(UbicacionProducto ubicacionProducto)
        {
            UbicacionManager manUbicacion = new UbicacionManager(context);
            DepositoManager manDeposito = new DepositoManager(context);
            string errores = null;

            if (!manDeposito.ValidarExistencia(ubicacionProducto.DepositoID))
                errores += errores + "Deposito no valido\n";
            if(!manUbicacion.FormatoCodUbicacionValido(ubicacionProducto.CodUbicacion))
                errores += errores + "Codigo de ubicación invalido\n";
            if(!manUbicacion.ValidarExistencia(ubicacionProducto.DepositoID,ubicacionProducto.CodUbicacion))
                errores += errores + "Deposito y Ubicación invalido\n";
            if(!manUbicacion.ProductoAlmacenadoEnDepositosAsync(ubicacionProducto.ProductoID).Result)
                errores += "Producto no almacenado en depositos\n";
            if (errores == null) {
                if (!this.ValidarCantidadDeProductosDistintos(ubicacionProducto))
                    errores += "Se excedio la cantidad maxima de productos diferentes en la ubicación "+ ubicacionProducto.CodUbicacion + " \n";
            }

            if (errores == null)
            {
                if (!this.ValidaStockMaximoEnUbicacion(ubicacionProducto))
                    errores += "Se excedio la cantidad maxima de unidades en la ubicación " + ubicacionProducto.CodUbicacion + " \n";
            }

            return errores;
        }

        public void AjustarCantidades(ref UbicacionProducto ubicacionProducto)
        {
            string deposito = ubicacionProducto.DepositoID;
            string ubicacion = ubicacionProducto.CodUbicacion;
            string producto = ubicacionProducto.ProductoID;

            UbicacionProducto ubicacionGuardada = context.UbicacionProducto.First(x => x.DepositoID == deposito && x.CodUbicacion == ubicacion && x.ProductoID == producto);
            ubicacionProducto.Cantidad += ubicacionGuardada.Cantidad; 

        }

        public string ValidarModficacion(UbicacionProducto ubicacionProducto)
        {
            string errores = null;
            if (!this.ExisteProductoEnUbicacion(ubicacionProducto.ProductoID, ubicacionProducto.DepositoID, ubicacionProducto.CodUbicacion))
                errores += "Registro a actualizar inexistente";
            else {

                UbicacionProducto ubicacionGuardada = context.UbicacionProducto.First(x => x.DepositoID == ubicacionProducto.DepositoID && x.CodUbicacion == ubicacionProducto.CodUbicacion && x.ProductoID == ubicacionProducto.ProductoID);
                if (ubicacionGuardada.Cantidad - ubicacionProducto.Cantidad < 0)
                    errores += "Stock insuficiente";
            }

            return errores;
        }

        public int ObtenerStockProducto(string depositoID, string codUbicacion, string productoID)
        {
            int stockActual = 0;
            UbicacionProducto ubicacionProducto = context.UbicacionProducto.First(x => x.DepositoID == depositoID && x.CodUbicacion == codUbicacion && x.ProductoID == productoID);
            if (ubicacionProducto != null)
                stockActual = ubicacionProducto.Cantidad;

            return stockActual;
        }

        public bool ExisteProductoEnUbicacion(string productoID, string depositoID, string codUbicacion)
        {
            return context.UbicacionProducto.Any(x => x.DepositoID == depositoID && x.CodUbicacion == codUbicacion && x.ProductoID == productoID);
        }

        public bool ValidaStockMaximoEnUbicacion(UbicacionProducto ubicacionProducto)
        {
            bool valido = true;
            List<UbicacionProducto> productosEnUbicacion = context.UbicacionProducto.Where(x => x.DepositoID == ubicacionProducto.DepositoID && x.CodUbicacion == ubicacionProducto.CodUbicacion).ToList();
            int cantidadDeUnidades = productosEnUbicacion.Sum(x => x.Cantidad);

            if (cantidadDeUnidades + ubicacionProducto.Cantidad > Constantes.CantidadMaximaDeUnidadesEnUbicacion)
                valido = false;

            return valido;
        }

        public bool ValidarCantidadDeProductosDistintos(UbicacionProducto ubicacionProducto)
        {
            bool valido = true;

            List<UbicacionProducto> productosEnUbicacion = context.UbicacionProducto.Where(x => x.DepositoID == ubicacionProducto.DepositoID && x.CodUbicacion == ubicacionProducto.CodUbicacion).ToList();
            if (!productosEnUbicacion.Any(x => x.ProductoID == ubicacionProducto.ProductoID)) {
                if (productosEnUbicacion.Count + 1 > Constantes.ProductosDiferentesEnUbicacion)
                    valido = false;
            }

            return valido;
        }

    }
}
