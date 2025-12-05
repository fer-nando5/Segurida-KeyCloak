using System.ComponentModel.DataAnnotations;

namespace System.Eclaim.Application.Dto.Reclamo
{
    public class ReclamoRequest
    {
        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Fecha de reclamo es obligatorio")]
        public DateTime FechaReclamo { get; set; }
        [Required(ErrorMessage = "El campo UsuarioRegistro es obligatorio")]
        public string UsuarioRegistro { get; set; } =string.Empty;
    }
}
