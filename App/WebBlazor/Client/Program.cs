using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebBlazor.Client;
using Microsoft.AspNetCore.Components.Authorization;
using AppContracts.RestApi;
using AppContracts;
using RestEase;
using RestEase.HttpClientFactory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// authentication state plumbing
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

// HTTP client configuration
builder.Services.AddTransient<AntiforgeryHandler>();


builder.Services.AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AntiforgeryHandler>();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

// builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
var appName = AppConstants.ProductApiName;

var appUri = $"http://{appName}:7003";

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();
builder.Services.AddRestEaseClient(typeof(AppContracts.RestApi.IProductApi), appUri, client =>
{
    client.RequestPathParamSerializer = new StringEnumRequestPathParamSerializer();
}).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// builder.Services.AddRestClient<CustomAuthorizationMessageHandler>(
//      typeof(IProductApi),
//                 AppConstants.ProductApiName,
//                 7003);

await builder.Build().RunAsync();
