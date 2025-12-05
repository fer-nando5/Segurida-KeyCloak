using System.Eclaim.Domain.Dpo;
using System.Text.Json;

namespace System.Eclaim.Domain.OutPort.Services
{
    public interface ICaptchaService
    {
        Task<JsonElement> RedeemAsync(Redeem request);
        Task<JsonElement> CreateChallengeAsync();
        Task<CaptchaVerifyDpo> VerifyCaptchaAsync(string token);
    }
}
