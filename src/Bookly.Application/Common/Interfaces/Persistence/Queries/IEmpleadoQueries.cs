using Bookly.Application.Empleados.Queries.GetById;
using Bookly.Application.Empleados.Queries.GetServicios;
using Bookly.Application.HorariosLaborales.Queries.GetById;

namespace Bookly.Application.Common.Interfaces.Persistence.Queries;

public interface IEmpleadoQueries
{
    Task<GetEmpleadoByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GetEmpleadoServicioResponse>> GetServiciosAsync(Guid empleadoId, CancellationToken cancellationToken);
    Task<IReadOnlyList<GetHorarioLaboralByIdResponse>> GetHorariosLaboralesAsync(Guid empleadoId, CancellationToken cancellationToken);
}
