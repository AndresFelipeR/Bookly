using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Application.Servicios.Queries.GetById;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Queries;

public sealed class ServicioQueries : IServicioQueries
{
    private readonly BooklyDbContext _context;

    public ServicioQueries(BooklyDbContext context)
    {
        _context = context;
    }

    public async Task<GetServicioByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Servicios
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetServicioByIdResponse(
                x.Id,
                x.Nombre,
                x.Descripcion,
                x.Precio.Amount,
                x.Precio.Currency,
                x.TipoServicio.Nombre,
                (int)x.Duracion.Value.TotalMinutes,
                (int)x.PoliticaReserva.MargenCancelacion.Value.TotalMinutes,
                (int)x.PoliticaReserva.MargenAnticipacion.Value.TotalMinutes))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
