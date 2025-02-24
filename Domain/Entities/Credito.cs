using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Credito
    {
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public decimal TasaInteres { get; set; }

        [Required]
        public int PlazoMeses { get; set; }

        public decimal SaldoPendiente { get; set; }

        [Required]
        public string Estado { get; set; } = "Pendiente"; // Estado inicial

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        public DateTime? FechaOtorgamiento { get; set; }

        public DateTime FechaVencimiento => FechaOtorgamiento?.AddMonths(PlazoMeses) ?? DateTime.Now.AddMonths(PlazoMeses);

        public List<Abono> Abonos { get; set; } = new();
    }
}
