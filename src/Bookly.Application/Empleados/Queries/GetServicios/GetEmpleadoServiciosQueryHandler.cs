using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.Empleados.Queries.GetServicios;

public sealed class GetEmpleadoServiciosQueryHandler
    : IRequestHandler<GetEmpleadoServiciosQuery, IReadOnlyList<GetEmpleadoServicioResponse>>
{
    private readonly IEmpleadoQueries _empleadoQueries;

    public GetEmpleadoServiciosQueryHandler(IEmpleadoQueries empleadoQueries)
    {
        _empleadoQueries = empleadoQueries;
    }

    public async Task<IReadOnlyList<GetEmpleadoServicioResponse>> Handle(
        GetEmpleadoServiciosQuery request,
        CancellationToken cancellationToken)
    {
        var existe = await _empleadoQueries.ExistsAsync(request.EmpleadoId, cancellationToken);
        if (!existe)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.EmpleadoId));

        return await _empleadoQueries.GetServiciosAsync(request.EmpleadoId, cancellationToken);
    }
}
