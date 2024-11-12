using InsideSistemas.Domain.Entities;
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

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.DataCriacao).IsRequired();
                entity.Property(p => p.EstaFechado).IsRequired();
                entity.HasMany(p => p.Produtos)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Preco).IsRequired();
                entity.Property(p => p.Quantidade).IsRequired();
            });
        }
    }
}
