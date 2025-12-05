using System.Eclaim.Application.InPorts.Captcha;
using System.Eclaim.Domain.OutPort.Services;
using System.Text.Json;

namespace System.Eclaim.Application.UseCases.Captcha
{
    public class CreateChallengeUseCase: ICreateChallengeUseCase
    {
        private readonly ICaptchaService _captchaService;
        public CreateChallengeUseCase(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        public async Task<JsonElement> ExecuteAsync()
        {
            return await _captchaService.CreateChallengeAsync();
        }
    }
}
