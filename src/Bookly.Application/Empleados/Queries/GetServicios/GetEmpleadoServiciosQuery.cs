using MediatR;

namespace Bookly.Application.Empleados.Queries.GetServicios;

public sealed record GetEmpleadoServiciosQuery(Guid EmpleadoId)
    : IRequest<IReadOnlyList<GetEmpleadoServicioResponse>>;
