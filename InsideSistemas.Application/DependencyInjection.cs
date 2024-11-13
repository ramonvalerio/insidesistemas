using InsideSistemas.Application.Pedidos;
using InsideSistemas.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPedidoAppService, PedidoAppService>();
            services.AddInfrastructure(configuration);

            return services;
        }
    }
}
