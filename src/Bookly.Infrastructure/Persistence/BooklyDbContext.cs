using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence;

public class BooklyDbContext : DbContext
{
    public BooklyDbContext(DbContextOptions<BooklyDbContext> options) : base(options)
    {
        
    }

    public DbSet<Servicio> Servicios { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Reserva> Reservas { get; set; } = null!;
    public DbSet<Empleado> Empleados { get; set; } = null!;
    public DbSet<TipoServicio> TiposServicio { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooklyDbContext).Assembly);
    }

}
