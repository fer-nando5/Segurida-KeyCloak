using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Eclaim.UI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace System.Eclaim.UI.Pages
{
    public partial class InsertaReclamo
    {
        [Inject] HttpClient HttpClient { get; set; } = default!;
        [Inject] IToastService Toast { get; set; } = default!;
        public InsertaReclamoRequest ReclamoRequest { get; set; } = new();
        private string? userId;
        private string? numeroReclamo;
        private bool IsReadOnly = false;
        private bool IsButtonDisabled = false;
        private string BotonClase= "btn-submit";

        public async Task OnInsertaReclamo()
        {
            try
            {
                //ReclamoRequest.UsuarioRegistro = userId!;
                ReclamoRequest.UsuarioRegistro = "8d78ee43-d478-499c-a4cb-2bf4d62c637d";
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/Reclamo/Registra")
                {
                    Content = JsonContent.Create(ReclamoRequest)
                };

                request.Headers.Add("X-Captcha-Token", _lastToken);
                request.SetBrowserRequestOption("credentials", "include");

                var response = await HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = await response.Content.ReadFromJsonAsync<BaseResponse<IdentityResponse>>(options);
                    Toast.ShowSuccess($"El reclamo {result.Result.Data.ToString()} se creo correctamente");
                    numeroReclamo = result.Result.Data.ToString();
                    IsReadOnly = true;
                    IsButtonDisabled = true;
                    BotonClase = "btn-submit disabled-btn";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Toast.ShowWarning("Usted no se encuentra autorizado");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Toast.ShowError($"Hubo un error al crear el reclamo: {error}");
                }
            }
            catch (Exception ex)
            {
                Toast.ShowError($"Hubo un error al crear el reclamo: {ex.Message}");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                userId = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value; 
            }

            ReclamoRequest.FechaReclamo = DateTime.Today;
        }
    }
}
