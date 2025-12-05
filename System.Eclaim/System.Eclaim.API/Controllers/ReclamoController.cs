using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Eclaim.API.Attributes;
using System.Eclaim.Application.Dto;
using System.Eclaim.Application.Dto.Reclamo;
using System.Eclaim.Application.InPorts.Reclamos;

namespace System.Eclaim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {

        private readonly ICreateReclamoUseCase _createReclamoUseCase;
        private readonly IFindByReclamoUseCase _findByReclamoUseCase;
        public ReclamoController(ICreateReclamoUseCase createReclamoUseCase, IFindByReclamoUseCase findByReclamoUseCase)
        {
            _createReclamoUseCase = createReclamoUseCase;
            _findByReclamoUseCase = findByReclamoUseCase;
        }

        [HttpPost("Registra")]
        [Authorize]
        [ValidateCaptcha]
        public async Task<IActionResult> Registra([FromBody] ReclamoRequest request)
        {
            var result = await _createReclamoUseCase.ExecuteAsync(request);
            return Ok(BaseResponse<IdentityResponse>.Success(result));
        }

        [HttpGet("Buscar/{code}")]
        [Authorize]
        public async Task<IActionResult> Get(string code)
        {
            var result = await _findByReclamoUseCase.ExecuteAsync(code);
            return Ok(BaseResponse<object>.Success(result));
        }
    }
}
