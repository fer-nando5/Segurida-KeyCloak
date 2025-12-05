using System.Eclaim.Domain.Dpo;
using System.Eclaim.Domain.OutPort.Secrets;
using System.Eclaim.Domain.OutPort.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace System.Eclaim.Infraestructure.Adapters.Services
{
    public class CaptchaService : ICaptchaService
    {

        private readonly HttpClient _httpClient;
        private readonly IVaultSecretsProvider _vaultService;
        public CaptchaService(HttpClient httpClient, IVaultSecretsProvider vaultSecretsProvider)
        {
            _httpClient = httpClient;
            _vaultService = vaultSecretsProvider;
        }

        public async Task<JsonElement> RedeemAsync(Redeem request)
        {
            var secrets = _vaultService.GetSecretsAsync().GetAwaiter().GetResult();
            var accessToken = secrets["AccessTokenCap"];
            var siteKey = secrets["SiteKeyCap"];
            var capUrl = secrets["ApiCap"];
            var url = $"{capUrl}{siteKey}/redeem";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"Bot {accessToken}");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error al obtener validar el challenge desde CAP");

            return JsonDocument.Parse(result).RootElement;
        }

        public async Task<JsonElement> CreateChallengeAsync()
        {
            var secrets = _vaultService.GetSecretsAsync().GetAwaiter().GetResult();
            var accessToken = secrets["AccessTokenCap"];
            var siteKey = secrets["SiteKeyCap"];
            var capUrl = secrets["ApiCap"];
            var url = $"{capUrl}{siteKey}/challenge";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"Bot {accessToken}");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync(url, null);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error al crear el challenge desde CAP");

            return JsonDocument.Parse(result).RootElement;
        }

        public async Task<CaptchaVerifyDpo> VerifyCaptchaAsync(string token)
        {
            var secrets = _vaultService.GetSecretsAsync().GetAwaiter().GetResult();
            var accessToken = secrets["AccessTokenCap"];
            var siteKey = secrets["SiteKeyCap"];
            var capUrl = secrets["ApiCap"];
            var tokenChallenge = secrets["ChallengeTokenCap"];
            var url = $"{capUrl}{siteKey}/siteverify";                                                                                                                            

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"Bot {accessToken}");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                secret = tokenChallenge,
                response = token
            };

            var response = await _httpClient.PostAsJsonAsync(url, payload);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error al verificar el token en CAP.");

            var verifyResponse = JsonSerializer.Deserialize<CaptchaVerifyDpo>(result,
               new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (verifyResponse is null)
                throw new HttpRequestException("Error al obtener respuesta del servicio verify captcha");

            return verifyResponse;
        }
    }
}
