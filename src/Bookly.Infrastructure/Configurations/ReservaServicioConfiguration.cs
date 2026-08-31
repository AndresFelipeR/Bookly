using Bookly.Domain.Entities;
using Bookly.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class ReservaServicioConfiguration : IEntityTypeConfiguration<ReservaServicio>
{
    public void Configure(EntityTypeBuilder<ReservaServicio> builder)
    {
        builder.ToTable("ReservaServicio");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.ServicioId).IsRequired();
        
        builder.Property(x => x.Nombre).HasMaxLength(200).IsRequired();

        builder.OwnsOne(x => x.Precio, money =>
        {
            money.Property(x => x.Amount)
                .HasColumnName("Precio")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(x => x.Currency)
                .HasColumnName("Moneda")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Duracion, duracion =>
        {
            duracion.Property(x => x.Value)
                .HasColumnName("Duracion")
                .IsRequired();
        });
    }
}