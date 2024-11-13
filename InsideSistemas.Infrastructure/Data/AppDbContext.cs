using InsideSistemas.Domain.Models;
using InsideSistemas.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InsideSistemas.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
        }
    }
}