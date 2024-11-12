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
            // Add DbContext configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Repositories
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}
