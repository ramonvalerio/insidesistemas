using InsideSistemas.Application.Pedidos;
using InsideSistemas.Domain.Models;
using InsideSistemas.Infrastructure.Data;
using InsideSistemas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, string connectionString)
        {
            // Application Services
            services.AddScoped<IPedidoAppService, PedidoAppService>();

            // Repositories
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            // Infrastructure Services
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(connectionString));

            return services;
        }
    }
}
