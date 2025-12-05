using System.ComponentModel.DataAnnotations;
using System.Eclaim.Domain.Exceptions;

namespace System.Eclaim.Domain.Entities
{
    public class Reclamo
    {
        public object maxNumero;

        [Key]
        public string Id { get; set; }
        [MaxLength(20)]
        public string NumeroReclamo { get; set; }  //RCL-20251011-001
        public DateTime FechaReclamo { get; set; }
        [MaxLength(200)]
        public string Descripcion { get; set; }
        public string UsuarioRegistro { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EsActivo { get; set; }


        public Reclamo() { }

        public Reclamo(string id, string numeroReclamo, DateTime fechaReclamo, string descripcion, string usuarioRegistro) 
        {
            Id = id;
            NumeroReclamo = numeroReclamo;
            FechaReclamo = fechaReclamo;
            Descripcion = descripcion;
            UsuarioRegistro = usuarioRegistro;
            FechaRegistro = DateTime.Now;
            EsActivo = true;
        }

        public static Reclamo Registrar(string numeroReclamo, DateTime fechaReclamo, string descripcion, string usuarioRegistro)
        {
            if (string.IsNullOrEmpty(numeroReclamo)) throw new DomainException("Numero Reclamo es requerido");
            if (string.IsNullOrEmpty(descripcion)) throw new DomainException("descripcion es requerido");

            var reclamo = new Reclamo(Guid.NewGuid().ToString(), numeroReclamo, fechaReclamo, descripcion, usuarioRegistro);
            return reclamo;
        }


    }
}
