using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure();
builder.Services.AddApplication();


var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(cs));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FGC Biblioteca API", Version = "v1" });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/v1/generos", async (
    ISender sender,
    Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Command cmd) =>
{
    var result = await sender.Send(cmd);

    IResult response = result.IsFailure
        ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
        : TypedResults.Created($"/v1/generos/{result.Value.Id}", result.Value);

    return response;

});

app.Run();