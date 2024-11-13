using InsideSistemas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsideSistemas.Infrastructure.Data.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DataCriacao).IsRequired();
            builder.Property(p => p.EstaFechado).IsRequired();
            builder.HasMany(p => p.Produtos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}