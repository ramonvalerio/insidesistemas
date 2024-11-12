﻿using InsideSistemas.Application.Services;
using InsideSistemas.Domain.Services;
using InsideSistemas.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, string connectionString)
        {
            // Application Services
            services.AddScoped<IPedidoAppService, PedidoAppService>();

            // Domain Services
            services.AddScoped<PedidoService, PedidoService>();

            // Infrastructure Services
            services.AddInfrastructureServices(connectionString);

            return services;
        }
    }
}
