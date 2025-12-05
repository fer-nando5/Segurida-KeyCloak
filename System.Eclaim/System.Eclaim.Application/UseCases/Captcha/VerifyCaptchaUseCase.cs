using System.Eclaim.Application.InPorts.Captcha;
using System.Eclaim.Domain.OutPort.Services;

namespace System.Eclaim.Application.UseCases.Captcha
{
    public class VerifyCaptchaUseCase: IVerifyCaptchaUseCase
    {
        private readonly ICaptchaService _captchaService;
        public VerifyCaptchaUseCase(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        public async Task<(bool IsValid, string Message)> ExecuteAsync(string token)
        {
            var result = await _captchaService.VerifyCaptchaAsync(token);

            if (result.Success)
                return (true, "Catpcha válido.");
            else
                return (false, "Catpcha inválido.");
        }
    }
}
