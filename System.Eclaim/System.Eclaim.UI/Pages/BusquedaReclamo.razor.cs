using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Eclaim.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace System.Eclaim.UI.Pages
{
    public partial class BusquedaReclamo
    {
        [Inject] HttpClient HttpClient { get; set; } = default!;
        [Inject] IToastService Toast { get; set; } = default!;

        private string? NumeroReclamo;
        private BusquedaReclamoResponse? ReclamoResponse;
        private bool BusquedaRealizada = false;

        private async Task onBuscarReclamo()
        {
            try
            { 
                BusquedaRealizada = false;
                ReclamoResponse = null;

                if (!string.IsNullOrWhiteSpace(NumeroReclamo))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"/Api/Reclamo/Buscar/{NumeroReclamo}");
                    request.SetBrowserRequestOption("credentials", "include");
                    var response = await HttpClient.SendAsync(request);


                    if (response.IsSuccessStatusCode)
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var result = await response.Content.ReadFromJsonAsync<BaseResponse<BusquedaReclamoResponse>>(options);
                        if (result != null && result.Result != null)
                        {
                            ReclamoResponse = result.Result;
                            Toast.ShowSuccess("Reclamo consultado exitosamente");
                        }
                        else
                        {
                            Toast.ShowSuccess($"No se encontró ningún reclamo con el número '{NumeroReclamo}'");
                        }
                    }
                    else
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        Toast.ShowSuccess($"No se encontró ningún reclamo con el número '{NumeroReclamo}'");
                    }

                }

                BusquedaRealizada = true;
            }
            catch (Exception ex)
            {
                Toast.ShowError($"Hubo un error al buscar el reclamo con el número '{NumeroReclamo}'");
            }
        }
    }
}
