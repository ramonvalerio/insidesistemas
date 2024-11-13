using InsideSistemas.Domain.Models;
using InsideSistemas.Infrastructure.Data;
using InsideSistemas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsideSistemas.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;

            services.
                AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(connectionString));

            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}
