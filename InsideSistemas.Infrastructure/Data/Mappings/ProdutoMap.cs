using InsideSistemas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsideSistemas.Infrastructure.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Preco).IsRequired();
            builder.Property(p => p.Quantidade).IsRequired();
        }
    }
}