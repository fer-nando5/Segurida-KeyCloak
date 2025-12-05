namespace System.Eclaim.Application.InPorts.Captcha
{
    public interface IVerifyCaptchaUseCase
    {
        Task<(bool IsValid, string Message)> ExecuteAsync(string token);
    }
}
