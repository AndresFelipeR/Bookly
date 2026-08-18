using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class HorarioLaboralConfiguration : IEntityTypeConfiguration<HorarioLaboral>
{
    public void Configure(EntityTypeBuilder<HorarioLaboral> builder)
    {
        builder.ToTable("HorarioLaboral");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasQueryFilter(x => x.State);
        builder.Property(x => x.State).IsRequired();

        builder.Property(x => x.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Descripcion)
            .IsRequired()
            .HasMaxLength(500);

        builder.OwnsMany(x => x.Detalles, detalle =>
        {
            detalle.ToTable("HorarioLaboralDetalle");
            detalle.WithOwner().HasForeignKey("HorarioLaboralId");
            detalle.HasKey(x => x.Id);
            detalle.Property(x => x.Id).ValueGeneratedNever();

            detalle.Property(x => x.Dia)
                .HasConversion<int>()
                .IsRequired();

            detalle.Property(x => x.HoraInicio)
                .HasColumnType("time")
                .IsRequired();

            detalle.Property(x => x.HoraFin)
                .HasColumnType("time")
                .IsRequired();
        });

        builder.Navigation(x => x.Detalles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
