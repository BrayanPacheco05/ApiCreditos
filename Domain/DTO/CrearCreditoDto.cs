using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    
        public class CrearCreditoDto
        {
            public int ClienteId { get; set; }
            public decimal Monto { get; set; }
            public decimal TasaInteres { get; set; }
            public int PlazoMeses { get; set; }
        }
    

}
