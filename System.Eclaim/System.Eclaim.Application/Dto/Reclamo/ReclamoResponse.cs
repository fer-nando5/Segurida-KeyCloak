namespace System.Eclaim.Application.Dto.Reclamo
{
    public class ReclamoResponse
    {
        public string Id { get; set; }
        public string NumeroReclamo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReclamo { get; set; }
        public string? UsuarioRegistro { get; set; }
    }
}
