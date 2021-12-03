using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlDeStock.Models
{
    public class Deposito
    {
        [Key]
        public string DepositoID { get; set; }
        public string Descripcion { get; set; }

    }
}
