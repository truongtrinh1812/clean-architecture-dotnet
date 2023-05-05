using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration configuration)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "https://ocelotwebapigatewayapp:7050" },
            scopes: new[] { "openid", "profile", "customer.fullaccess", "product.fullaccess", "setting.fullaccess", "appgateway.fullaccess", "offline_access" });
    }
}
