using System.Eclaim.Application.Dto;
using System.Eclaim.Application.Dto.Reclamo;
using System.Eclaim.Application.InPorts.Reclamos;
using System.Eclaim.Domain.Entities;
using System.Eclaim.Domain.OutPort.Persistence;

namespace System.Eclaim.Application.UseCases.Reclamos
{
    public class CreateReclamoUseCase : ICreateReclamoUseCase
    {
        private readonly IReclamoRepository _reclamoRepository;
        public CreateReclamoUseCase(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<IdentityResponse> ExecuteAsync(ReclamoRequest request)
        {
            var codigo = await CodigoGenerado();
            var reclamo = Reclamo.Registrar(codigo, request.FechaReclamo, request.Descripcion, request.UsuarioRegistro);
            var result = await _reclamoRepository.CreateAsync(reclamo);

            return new IdentityResponse
            {
                Data = reclamo.NumeroReclamo,
                Success = result.Success,
                Errors = result.Errors
            };
        }

        private async Task<string> CodigoGenerado()
        {
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            int Correlativo = await _reclamoRepository.MaximoCorrelativoAsync();
            return "RCL-" + fecha + "-" + (Correlativo + 1).ToString("D3");

        }
    }
}
