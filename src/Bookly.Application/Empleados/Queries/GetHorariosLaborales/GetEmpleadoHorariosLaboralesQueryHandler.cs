using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Application.HorariosLaborales.Queries.GetById;
using MediatR;

namespace Bookly.Application.Empleados.Queries.GetHorariosLaborales;

public sealed class GetEmpleadoHorariosLaboralesQueryHandler
    : IRequestHandler<GetEmpleadoHorariosLaboralesQuery, IReadOnlyList<GetHorarioLaboralByIdResponse>>
{
    private readonly IEmpleadoQueries _empleadoQueries;

    public GetEmpleadoHorariosLaboralesQueryHandler(IEmpleadoQueries empleadoQueries)
    {
        _empleadoQueries = empleadoQueries;
    }

    public async Task<IReadOnlyList<GetHorarioLaboralByIdResponse>> Handle(
        GetEmpleadoHorariosLaboralesQuery request,
        CancellationToken cancellationToken)
    {
        var existe = await _empleadoQueries.ExistsAsync(request.EmpleadoId, cancellationToken);
        if (!existe)
            throw new NotFoundException(EmpleadoErrors.NotFound(request.EmpleadoId));

        return await _empleadoQueries.GetHorariosLaboralesAsync(request.EmpleadoId, cancellationToken);
    }
}
