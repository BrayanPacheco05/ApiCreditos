using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class ClienteCreateDTO
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public int UsuarioId { get; set; }
    }


}
