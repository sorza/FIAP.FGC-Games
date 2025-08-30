using Fgc.Api.Common.Api;
using Fgc.Api.Endpoints;
using Fgc.Infrastructure.Compartilhado.Data.Contexts;
using Microsoft.EntityFrameworkCore;

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