using System.Eclaim.Application.Dto.Reclamo;
using System.Eclaim.Application.InPorts.Reclamos;
using System.Eclaim.Domain.OutPort.Persistence;

namespace System.Eclaim.Application.UseCases.Reclamos
{
    public class FindByReclamoUseCase: IFindByReclamoUseCase
    {

        private readonly IReclamoRepository _reclamoRepository;
        public FindByReclamoUseCase(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<ReclamoResponse> ExecuteAsync(string NumeroReclamo)
        {
            var reclamo = await _reclamoRepository.FindByCodeAsync(NumeroReclamo);

            if (reclamo is null)
            {
                throw new ApplicationException($"Reclamo con código {NumeroReclamo} no encontrado");
            }

            return new ReclamoResponse
            {
                Id = reclamo.Id,
                NumeroReclamo = reclamo.NumeroReclamo,
                Descripcion = reclamo.Descripcion,
                FechaReclamo = reclamo.FechaReclamo,
                UsuarioRegistro = reclamo.UsuarioRegistro
            };
        }
    }
}
