using System.Eclaim.Application.Dto.Captcha;
using System.Eclaim.Application.InPorts.Captcha;
using System.Eclaim.Domain.Dpo;
using System.Eclaim.Domain.OutPort.Services;
using System.Text.Json;
using Mapster;

namespace System.Eclaim.Application.UseCases.Captcha
{
    public class RedeemUseCase: IRedeemUseCase
    {
        private readonly ICaptchaService _captchaService;
        public RedeemUseCase(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        public async Task<JsonElement> ExecuteAsync(RedeemRequest request)
        {
            return await _captchaService.RedeemAsync(request.Adapt<Redeem>());
        }
    }
}
