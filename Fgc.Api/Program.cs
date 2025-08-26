using Fgc.Api.Common.Api;
using Fgc.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.ConfigureDevEnvironment();

app.UseSecurity();
app.MapEndpoints();

app.Run("http://0.0.0.0:5000");



