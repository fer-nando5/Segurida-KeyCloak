using Microsoft.AspNetCore.Mvc;
using System.Eclaim.Application.Dto.Captcha;
using System.Eclaim.Application.InPorts.Captcha;

namespace System.Eclaim.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly IRedeemUseCase _useRedeem;
        private readonly ICreateChallengeUseCase _useChallenge;
        public CaptchaController(IRedeemUseCase redeemUse, ICreateChallengeUseCase challengeUseCase)
        {
            _useRedeem = redeemUse;
            _useChallenge = challengeUseCase;
        }

        [HttpPost("redeem")]
        public async Task<IActionResult> Redeem([FromBody] RedeemRequest request)
        {
            var result = await _useRedeem.ExecuteAsync(request);
            return Ok(result);
        }

        [HttpPost("challenge")]
        public async Task<IActionResult> CreateChallenge()
        {
            var result = await _useChallenge.ExecuteAsync();
            return Ok(result);
        }
    }
}
