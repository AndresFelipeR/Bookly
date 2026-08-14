using MediatR;

namespace Bookly.Application.TipoServicios.Queries.GetById;

public sealed record GetTiposServicioByIdQuery(Guid Id) : IRequest<GetTiposServicioByIdResponse>
{
    
}