using Fgc.Api.Common.Api;
using Fgc.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
app.ConfigureDevEnvironment();

app.UseSecurity();
app.MapEndpoints();

app.Run();


