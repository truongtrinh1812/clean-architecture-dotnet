using CustomerService.Infra;
using ApiAnchor = CustomerService.Api.Anchor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor));

var app = builder.Build();
app.UseCoreApplication(builder.Environment);

app.Run();
