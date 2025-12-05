using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace System.Eclaim.UI
{
    public class ApiAuthorizationInterceptor : AuthorizationMessageHandler
    {
        public ApiAuthorizationInterceptor(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:7128", "https://localhost:7224" },
                scopes: new[] { "openid", "profile" }
            );
        }
    }
}
