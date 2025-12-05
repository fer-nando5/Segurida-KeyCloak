using System.ComponentModel.DataAnnotations;

namespace System.Eclaim.UI.Models
{
    public class InsertaReclamoRequest
    {
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Fecha Reclamo es obligatorio")]
        public DateTime FechaReclamo { get; set; }
        [Required(ErrorMessage = "El campo UsuarioRegistro es obligatorio")]
        public string UsuarioRegistro { get; set; }
    }
}
