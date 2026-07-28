using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class TipoServicioRepository : ITipoServicioRepository
{
    private readonly BooklyDbContext _context;

    public TipoServicioRepository(BooklyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TipoServicio tipoServicioservicio, CancellationToken cancellationToken)
    {
        await _context.TiposServicio.AddAsync(tipoServicioservicio, cancellationToken);
    }

    public async Task<TipoServicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TiposServicio.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(TipoServicio tipoServicio, CancellationToken cancellationToken)
    {
        _context.TiposServicio.Update(tipoServicio);
        return Task.CompletedTask;
    }
}