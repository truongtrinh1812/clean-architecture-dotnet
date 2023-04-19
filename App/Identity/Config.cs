using Duende.IdentityServer.Models;

namespace Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[] { new IdentityResources.OpenId(), new IdentityResources.Profile(), };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("customer.fullaccess"),
            new ApiScope("customer.read"),
            new ApiScope("customer.write"),
            new ApiScope("product.fullaccess"),
            new ApiScope("setting.fullaccess"),
            new ApiScope("appgateway.fullaccess")
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("customer", "Customer API") { Scopes = { "customer.fullaccess" } },
            new ApiResource("product", "Product API") { Scopes = { "product.fullaccess" } },
            new ApiResource("setting", "Setting API") { Scopes = { "setting.fullaccess" } },
            new ApiResource("appgateway", "App Gateway") { Scopes = { "appgateway.fullaccess" } },
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "spa",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RedirectUris = { "https://localhost:5002/signin-oidc" },
                BackChannelLogoutUri = "https://localhost:5002/bff/backchannel",
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    "openid",
                    "profile",
                    "customer.fullaccess",
                    "product.fullaccess",
                    "setting.fullaccess",
                    "appgateway.fullaccess"
                }
            },
        };
}
