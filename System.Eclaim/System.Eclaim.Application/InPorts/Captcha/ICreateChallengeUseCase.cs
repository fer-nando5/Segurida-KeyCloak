using System.Text.Json;

namespace System.Eclaim.Application.InPorts.Captcha
{
    public interface ICreateChallengeUseCase
    {
        Task<JsonElement> ExecuteAsync();
    }
}
