using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class EmpleadoHorarioLaboralConfiguration : IEntityTypeConfiguration<EmpleadoHorarioLaboral>
{
    public void Configure(EntityTypeBuilder<EmpleadoHorarioLaboral> builder)
    {
        builder.ToTable("EmpleadoHorarioLaboral");
        builder.HasKey(x => new { x.EmpleadoId, x.HorarioLaboralId });

        builder.HasOne(x => x.Empleado)
            .WithMany(e => e.HorariosLaborales)
            .HasForeignKey(x => x.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.HorarioLaboral)
            .WithMany()
            .HasForeignKey(x => x.HorarioLaboralId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(x => x.HorarioLaboral.State);
    }
}
