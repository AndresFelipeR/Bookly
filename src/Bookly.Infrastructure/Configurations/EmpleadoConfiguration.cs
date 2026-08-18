using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleados");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.HasQueryFilter(x => x.State);
        builder.Property(x => x.State).IsRequired();

        builder.OwnsOne(x => x.NombreCompleto, nombre =>
        {
            nombre.Property(x => x.Nombre)
                .HasColumnName("Nombre")
                .IsRequired()
                .HasMaxLength(100);

            nombre.Property(x => x.Apellido)
                .HasColumnName("Apellido")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(254);
        });

        builder.OwnsOne(x => x.Telefono, telefono =>
        {
            telefono.Property(x => x.Value)
                .HasColumnName("Telefono")
                .IsRequired()
                .HasMaxLength(30);
        });

        builder.HasMany(x => x.Servicios)
            .WithMany(x => x.Empleados)
            .UsingEntity<Dictionary<string, object>>(
                "EmpleadoServicios",
                r => r
                    .HasOne<Servicio>()
                    .WithMany()
                    .HasForeignKey("ServicioId")
                    .OnDelete(DeleteBehavior.Cascade),
                l => l
                    .HasOne<Empleado>()
                    .WithMany()
                    .HasForeignKey("EmpleadoId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("EmpleadoId", "ServicioId");
                    j.ToTable("EmpleadoServicios");
                });

        builder.Navigation(x => x.HorariosLaborales)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
