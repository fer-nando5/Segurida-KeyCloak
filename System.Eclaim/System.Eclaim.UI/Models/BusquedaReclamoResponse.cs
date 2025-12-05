namespace System.Eclaim.UI.Models
{
    public class BusquedaReclamoResponse
    {
        public string Id { get; set; } = string.Empty;
        public string NumeroReclamo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaReclamo { get; set; }
        public string? UsuarioRegistro { get; set; }
    }
}
