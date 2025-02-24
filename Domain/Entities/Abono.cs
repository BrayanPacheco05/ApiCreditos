using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Abono
    {
        public int Id { get; set; }

        [ForeignKey("Credito")]
        public int CreditoId { get; set; }
        public Credito Credito { get; set; }

        [Required]
        public decimal Monto { get; set; }

        public DateTime FechaAbono { get; set; } = DateTime.Now;

        [Required]
        public string MetodoPago { get; set; } // Puede ser 'Efectivo', 'Transferencia', 'Tarjeta'
    }
}
