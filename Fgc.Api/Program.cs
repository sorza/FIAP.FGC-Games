using Fgc.Api.Endpoints;
using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(cs));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fgc.Api v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapEndpoints();

app.Run();