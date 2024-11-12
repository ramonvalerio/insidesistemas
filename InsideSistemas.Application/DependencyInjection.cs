using InsideSistemas.Application.Pedidos;
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

            // Infrastructure Services
            services.AddInfrastructureServices(connectionString);

            return services;
        }
    }
}
