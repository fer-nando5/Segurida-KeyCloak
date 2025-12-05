using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Eclaim.Domain.Dpo
{
    public class ReclamoResponseDpo
    {
        public string Id { get; set; }
        public string NumeroReclamo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReclamo { get; set; }
        public string? UsuarioRegistro { get; set; }
    }
}
