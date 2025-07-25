﻿using Fgc.Application.Compartilhado.Comportamentos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Fgc.Application.Compartilhado
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                x.AddOpenBehavior(typeof(ValidationBehavior<,>));
                x.AddOpenBehavior(typeof(TransactionBehavior<,>));
                x.AddOpenBehavior(typeof(LoggingBehavior<,>));

            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
