using Fgc.Api.Endpoints;
using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(cs));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} retornou {StatusCode} em {Elapsed:0.0000} ms";
});


app.Use(async (ctx, next) =>
{
    var correlationId = ctx.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                        ?? Guid.NewGuid().ToString();
    ctx.Response.Headers["X-Correlation-ID"] = correlationId;
    using (LogContext.PushProperty("CorrelationId", correlationId))
    {
        await next();
    }
});

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
Log.CloseAndFlush();