using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.HorariosLaborales.Queries.GetById;

public sealed class GetHorarioLaboralByIdQueryHandler
    : IRequestHandler<GetHorarioLaboralByIdQuery, GetHorarioLaboralByIdResponse>
{
    private readonly IHorarioLaboralQueries _horarioLaboralQueries;

    public GetHorarioLaboralByIdQueryHandler(IHorarioLaboralQueries horarioLaboralQueries)
    {
        _horarioLaboralQueries = horarioLaboralQueries;
    }

    public async Task<GetHorarioLaboralByIdResponse> Handle(
        GetHorarioLaboralByIdQuery request,
        CancellationToken cancellationToken)
    {
        var horarioLaboral = await _horarioLaboralQueries.GetByIdAsync(request.Id, cancellationToken);
        if (horarioLaboral is null)
            throw new NotFoundException(HorarioLaboralErrors.NotFound(request.Id));

        return horarioLaboral;
    }
}
