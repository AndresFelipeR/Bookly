using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class HorarioLaboralRepository : BaseRepository<HorarioLaboral>, IHorarioLaboralRepository
{
    public HorarioLaboralRepository(BooklyDbContext context) : base(context)
    {
    }

    public new async Task<HorarioLaboral?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.HorariosLaborales
            .Include(x => x.Detalles)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
