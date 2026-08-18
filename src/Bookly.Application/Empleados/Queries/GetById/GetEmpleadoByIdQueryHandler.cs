using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.Empleados.Queries.GetById;

public class GetEmpleadoByIdQueryHandler : IRequestHandler<GetEmpleadoByIdQuery, GetEmpleadoByIdResponse>
{
    private readonly IEmpleadoQueries _empleadoQueries;

    public GetEmpleadoByIdQueryHandler(IEmpleadoQueries empleadoQueries)
    {
        _empleadoQueries = empleadoQueries;
    }

    public async Task<GetEmpleadoByIdResponse> Handle(GetEmpleadoByIdQuery request, CancellationToken cancellationToken)
    {
        var empleado = await _empleadoQueries.GetByIdAsync(request.Id, cancellationToken);
        if (empleado is null)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.Id));

        return empleado;
    }
}
