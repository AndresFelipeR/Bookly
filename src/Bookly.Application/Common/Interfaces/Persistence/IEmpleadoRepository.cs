using Bookly.Domain.Entities;

namespace Bookly.Application.Common.Interfaces.Persistence;

public interface IEmpleadoRepository : IBaseRepository<Empleado>
{
    Task<Empleado?> GetByIdWithServiciosAsync(Guid id, CancellationToken cancellationToken);
    Task<Empleado?> GetByIdWithHorariosLaboralesAsync(Guid id, CancellationToken cancellationToken);
}
