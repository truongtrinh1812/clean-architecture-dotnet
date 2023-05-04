using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace WebBlazor.Client.BFF
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigation)
            : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://identityapp:7001" },
                scopes: new[] { "openid", "profile", "customer.fullaccess", "product.fullaccess", "setting.fullaccess", "appgateway.fullaccess" });
        }
    }
}