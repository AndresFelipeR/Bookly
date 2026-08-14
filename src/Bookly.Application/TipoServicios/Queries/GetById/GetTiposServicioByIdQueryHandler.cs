using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.TipoServicios.Queries.GetById;

public sealed class GetTiposServicioByIdQueryHandler : IRequestHandler<GetTiposServicioByIdQuery,GetTiposServicioByIdResponse>
{
    private readonly ITiposServicioQueries _tiposServicioQueries;

    public GetTiposServicioByIdQueryHandler(ITiposServicioQueries tiposServicioQueries)
    {
        _tiposServicioQueries = tiposServicioQueries;
    }

    public async Task<GetTiposServicioByIdResponse> Handle(GetTiposServicioByIdQuery request, CancellationToken cancellationToken)
    {
        var tiposServicio = await _tiposServicioQueries.GetByIdAsync(request.Id, cancellationToken);
        if (tiposServicio is null)
            throw new NotFoundException(TipoServicioErrors.NotFound(request.Id));

        return tiposServicio;
    }
}