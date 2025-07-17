using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.AspNetCore.Mvc;
using Fgc.Application.Biblioteca.CasosDeUso.Generos.Buscar;

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
    Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Command cmd,
    CancellationToken cancellationToken) =>
{
    var result = await sender.Send(cmd, cancellationToken);

    IResult response = result.IsFailure
        ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
        : TypedResults.Created($"/v1/generos/{result.Value.Id}", result.Value);

    return response;

});

app.MapGet("/v1/generos/{id}", async (
    ISender sender,
    string id,
    CancellationToken cancellationToken) =>
{
    if (!Guid.TryParse(id, out var guid))
        return TypedResults.BadRequest(new
        {
            Code = "404",
            Message = $"'{id}' não é um id válido."
        });

    var result = await sender.Send(new Query(guid), cancellationToken);

    IResult response = result.IsFailure
     ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
     : TypedResults.Ok(result.Value);  
    
    return response;
});

app.MapPut("/v1/generos", async (
    ISender sender,
    Fgc.Application.Biblioteca.CasosDeUso.Generos.Atualizar.Command cmd,
    CancellationToken cancellationToken) =>
{
    if (!Guid.TryParse(cmd.id, out var guid))
        return TypedResults.BadRequest(new
        {
            Code = "404",
            Message = $"'{cmd.id}' não é um id válido."
        });

    var result = await sender.Send(cmd, cancellationToken);

    IResult response = result.IsFailure
        ? TypedResults.Conflict(new { result.Error.Code, result.Error.Message })
        : TypedResults.Created($"/v1/generos/{result.Value.Id}", result.Value);

    return response;

});

app.Run();