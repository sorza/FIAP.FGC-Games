using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();

app.MapPost("/v1/generos", async (
    ISender sender,
    Fgc.Application.Biblioteca.CasosDeUso.Generos.Criar.Command command) =>
    {
        var result = await sender.Send(command);
        return TypedResults.Created($"v1/generos/{result.Value.Id}", result);
    });

app.Run();
