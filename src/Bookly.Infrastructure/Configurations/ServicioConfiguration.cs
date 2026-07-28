using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Bookly.Infrastructure.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {

        builder.ToTable("Servicios");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Descripcion).HasMaxLength(500);
        
        builder.OwnsOne(x => x.Precio, precio =>
        {
           precio.Property(p => p.Amount).HasColumnName("Precio_Amount").HasPrecision(18, 2);
           precio.Property(p => p.Currency).HasColumnName("Precio_Currency").HasMaxLength(3);
        });

        builder.OwnsOne(d => d.Duracion, duracion =>
        {
            duracion.Property(d => d.Value).HasColumnName("Duracion");
        });

       
        builder.OwnsOne(po => po.PoliticaReserva, politica =>
        {
           politica.OwnsOne( x => x.MargenAnticipacion, margen =>
           {
                margen.Property(m => m.Value).HasColumnName("MargenAnticipacion");
           });

           politica.OwnsOne( x => x.MargenCancelacion, margen =>
           {
                margen.Property(m => m.Value).HasColumnName("MargenCancelacion");
           });
        });

        builder.HasOne(x => x.TipoServicio)
               .WithMany()
               .HasForeignKey(x => x.TipoServicioId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.State).IsRequired();
        
    }
}
