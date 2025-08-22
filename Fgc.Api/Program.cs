using Fgc.Api.Endpoints;
using Fgc.Api.Utils.JsonLogs;
using Fgc.Application.Compartilhado;
using Fgc.Infrastructure.Compartilhado;
using Fgc.Infrastructure.Compartilhado.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Events;

var arraySink = new JsonArray(
    path: "logs/log.json",
    formatter: new FormatadorIndentado()
);

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()                         
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Destructure.ByTransforming<Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar.Command>(cmd => new {
        cmd.email,
        Senha = "***SECRET***"
    })
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ServiceName", "Fgc.Api")
    .WriteTo.Sink(arraySink)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
            Convert.FromBase64String(builder.Configuration["Jwt:Key"]!))

        };

    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SomenteAdmin", policy =>
        policy.RequireRole("Admin"));
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(cs));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{   
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fgc.Api",
        Version = "v1"
    });
    
    c.CustomSchemaIds(n => n.FullName);
   
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Digite: Bearer {seu_token}"
    });
        
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    var correlationId = ctx.Request.Headers["X-Correlation-ID"]
                          .FirstOrDefault()
                      ?? Guid.NewGuid().ToString();
    ctx.Response.Headers["X-Correlation-ID"] = correlationId;
    using (LogContext.PushProperty("CorrelationId", correlationId))
        await next();
});

app.UseSerilogRequestLogging(opts =>
{
    opts.MessageTemplate =
      "HTTP {RequestMethod} A requisicao {RequestPath} retornou {StatusCode} em {Elapsed:0.0000} ms";
    
    opts.GetLevel = (httpContext, elapsed, exception) =>
    {
        if (exception is not null)
            return LogEventLevel.Error;

        var status = httpContext.Response.StatusCode;
        if (status >= 500)
            return LogEventLevel.Error;
        if (status >= 400)
            return LogEventLevel.Warning;

        return LogEventLevel.Information;
    };
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fgc.Api v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();

Log.CloseAndFlush();


