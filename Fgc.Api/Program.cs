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

var app = builder.Build();

app.MapEndpoints();

#region Genero - Endpoints
/*
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

app.MapDelete("/v1/generos/{id}", async (
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

    var result = await sender.Send(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Remover.Command(guid), cancellationToken);
    IResult response = result.IsFailure
        ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
        : TypedResults.Ok(result.Value);
    return response;
});

app.MapGet("/v1/generos", async (
    ISender sender,
    CancellationToken cancellationToken) =>
{

    var result = await sender.Send(new Fgc.Application.Biblioteca.CasosDeUso.Generos.Listar.Query(), cancellationToken);
    IResult response = result.IsFailure
        ? TypedResults.NotFound(new { result.Error.Code, result.Error.Message })
        : TypedResults.Ok(result.Value);
    return response;
});*/
#endregion

app.Run();