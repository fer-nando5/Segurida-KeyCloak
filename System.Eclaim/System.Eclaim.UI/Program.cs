using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Eclaim.UI;
//using System.Eclaim.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "http://localhost:8080/realms/galaxy_realm";
    options.ProviderOptions.ClientId = "Blazor_Cliente";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
});

builder.Services.AddScoped<ApiAuthorizationInterceptor>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<ApiAuthorizationInterceptor>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7224") };
});

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();

//var backendUrl = builder.Configuration.GetValue<string>("Services:UrlBackend");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl!) });


//builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();

//builder.Services.AddAuthorizationCore();

//await builder.Build().RunAsync();
