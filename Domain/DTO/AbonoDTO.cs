using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AbonoDTO
    {
        public int CreditoId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
    }

}
