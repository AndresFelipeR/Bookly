using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class ServicioRepository : IServicioRepository
{
    private readonly BooklyDbContext _context;

    public ServicioRepository(BooklyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Servicio servicio, CancellationToken cancellationToken)
    {
        await _context.Servicios.AddAsync(servicio, cancellationToken);
    }

    public async Task<Servicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Servicios
            .Include(x => x.TipoServicio)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken)
    {
       _context.Servicios.Update(servicio);
       return Task.CompletedTask;
    }

}