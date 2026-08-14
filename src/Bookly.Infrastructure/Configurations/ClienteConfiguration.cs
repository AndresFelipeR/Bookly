using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.Infrastructure.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasQueryFilter(x => x.State);
        builder.Property(x => x.Activo).IsRequired();
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
    }
}