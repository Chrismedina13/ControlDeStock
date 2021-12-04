using ControlDeStock.Context;
using ControlDeStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Managers
{
    public class UbicacionManager
    {
        private AppDBContext context;

        public UbicacionManager(AppDBContext context)
        {
            this.context = context;
        }

        public string Validar(Ubicacion ubicacion)
        {
            Deposito deposito = context.Deposito.Where(x => x.DepositoID == ubicacion.DepositoID).FirstOrDefault();
            string errores = null;
            if (deposito == null)
                errores = "Deposito no valido\n";
            if (!this.FormatoCodUbicacionValido(ubicacion.CodUbicacion))
                errores = errores + "Codigo de ubicación invalido";

            return errores;                                                    
        }

        private bool FormatoCodUbicacionValido(string codUbicacion)
        {
            bool valido = true;
            var partesPatron = codUbicacion.Split("-").ToList();
            if (partesPatron.Count != 4)
                valido = false;
            
            if (valido) {
               
                foreach (string parte in partesPatron)
                {
                    if (parte.Length != 2)
                        valido = false;
                }
            }

            return valido;

        }
    }
}
