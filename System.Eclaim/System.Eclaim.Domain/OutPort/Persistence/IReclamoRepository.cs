using System.Eclaim.Domain.Dpo;
using System.Eclaim.Domain.Entities;

namespace System.Eclaim.Domain.OutPort.Persistence
{
    public interface IReclamoRepository
    {
        Task<OperationResult> CreateAsync(Reclamo reclamo);
        Task<ReclamoResponseDpo?> FindByCodeAsync(string numeroReclamo);
        Task<int> MaximoCorrelativoAsync();
    }
}
