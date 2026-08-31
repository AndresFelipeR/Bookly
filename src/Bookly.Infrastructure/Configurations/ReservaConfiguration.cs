using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class ReservaConfiguration :IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasQueryFilter(x => x.State);

        builder.Property(x => x.Estado)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("Estado")
            .HasMaxLength(20);

        builder.Property(x => x.FechaReserva)
            .HasColumnName("FechaReserva")
            .IsRequired();
        
        builder.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Servicios)
            .WithOne()
            .HasForeignKey("ReservaId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}