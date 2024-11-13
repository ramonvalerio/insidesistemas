using InsideSistemas.Application.Pedidos;
using InsideSistemas.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string databaseName)
        {
            services.AddScoped<IPedidoAppService, PedidoAppService>();
            services.AddInfrastructureServices(databaseName);

            return services;
        }
    }
}
