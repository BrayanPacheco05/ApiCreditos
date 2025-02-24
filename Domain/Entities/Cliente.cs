using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public int UsuarioId { get; set; } // Esta propiedad va a referenciar al usuario
        public Usuario Usuario { get; set; } // Relación con la entidad Usuario
        public List<Credito> Creditos { get; set; } = new();
    }
}
