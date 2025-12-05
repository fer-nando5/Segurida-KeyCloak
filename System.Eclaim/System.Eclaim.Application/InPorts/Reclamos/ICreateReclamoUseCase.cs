using System.Eclaim.Application.Dto;
using System.Eclaim.Application.Dto.Reclamo;

namespace System.Eclaim.Application.InPorts.Reclamos
{
    public interface ICreateReclamoUseCase
    {
        Task<IdentityResponse> ExecuteAsync(ReclamoRequest request);
    }
}
