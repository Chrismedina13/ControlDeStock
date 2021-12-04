using ControlDeStock.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Managers
{
    public class DepositoManager
    {
        private AppDBContext context;

        public DepositoManager(AppDBContext context)
        {
            this.context = context;
        }

        public bool ValidarExistencia(string codDeposito) {

            return context.Deposito.Any(x => x.DepositoID == codDeposito);
        }
    }
}
