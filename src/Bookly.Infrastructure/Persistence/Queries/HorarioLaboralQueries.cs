using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Application.HorariosLaborales.Queries.GetById;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Queries;

public sealed class HorarioLaboralQueries : IHorarioLaboralQueries
{
    private readonly BooklyDbContext _context;

    public HorarioLaboralQueries(BooklyDbContext context)
    {
        _context = context;
    }

    public async Task<GetHorarioLaboralByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.HorariosLaborales
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetHorarioLaboralByIdResponse(
                x.Id,
                x.Nombre,
                x.Descripcion,
                x.Detalles
                    .OrderBy(d => d.Dia)
                    .ThenBy(d => d.HoraInicio)
                    .Select(d => new GetHorarioLaboralDetalleResponse(
                        d.Id,
                        d.Dia,
                        d.HoraInicio,
                        d.HoraFin))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
