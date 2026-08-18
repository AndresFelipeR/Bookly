using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class EmpleadoRepository : BaseRepository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(BooklyDbContext context) : base(context)
    {
    }

    public async Task<Empleado?> GetByIdWithServiciosAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Empleados
            .Include(x => x.Servicios)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Empleado?> GetByIdWithHorariosLaboralesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Empleados
            .Include(x => x.HorariosLaborales)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
