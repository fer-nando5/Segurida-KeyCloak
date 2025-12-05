using System.Eclaim.Application.Dto.Reclamo;

namespace System.Eclaim.Application.InPorts.Reclamos
{
    public interface IFindByReclamoUseCase
    {
        Task<ReclamoResponse> ExecuteAsync(string NumeroReclamo);
    }
}
