using System.Eclaim.Application.Dto.Captcha;
using System.Text.Json;

namespace System.Eclaim.Application.InPorts.Captcha
{
    public interface IRedeemUseCase
    {
        Task<JsonElement> ExecuteAsync(RedeemRequest request);
    }
}
