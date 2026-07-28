using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class TipoServicioConfiguration : IEntityTypeConfiguration<TipoServicio>
{
    public void Configure(EntityTypeBuilder<TipoServicio> builder)
    {
        builder.ToTable("TipoServicio");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasQueryFilter(x => x.State);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(500);
        
    }
}