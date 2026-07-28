using Bookly.Application.Common;
using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.Servicios.Queries.GetById;

public sealed class GetServicioByIdQueryHandler : IRequestHandler<GetServicioByIdQuery, Result<GetServicioByIdResponse>>
{
    private readonly IServicioQueries _servicioQueries;

    public GetServicioByIdQueryHandler(IServicioQueries servicioQueries)
    {
        _servicioQueries = servicioQueries;
    }

    public async Task<Result<GetServicioByIdResponse>> Handle(GetServicioByIdQuery request, CancellationToken cancellationToken)
    {
        var servicio = await _servicioQueries.GetByIdAsync(request.Id, cancellationToken);
        if (servicio is null)
            return Result<GetServicioByIdResponse>.Failure(ServicioErrors.NotFound(request.Id));

        return Result<GetServicioByIdResponse>.Success(servicio);
    }
}
