using InsideSistemas.Domain.Repositories;
using InsideSistemas.Infrastructure.Data;
using InsideSistemas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InsideSistemasDBInMemory"));

            // Add Repositories
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}
