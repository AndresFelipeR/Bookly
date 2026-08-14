using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Application.Servicios.Queries.GetById;
using Bookly.Application.TipoServicios.Queries.GetById;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Queries;

public sealed class TiposServicioQueries : ITiposServicioQueries
{
    private readonly BooklyDbContext _context;

    public TiposServicioQueries(BooklyDbContext context)
    {
        _context = context;
    }

    public async Task<GetTiposServicioByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TiposServicio
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetTiposServicioByIdResponse(
                x.Id,
                x.Nombre,
                x.Descripcion
                ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}