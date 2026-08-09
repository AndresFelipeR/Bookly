using Bookly.Application.Common;
using MediatR;

namespace Bookly.Application.Servicios.Queries.GetById;

public sealed record GetServicioByIdQuery(Guid Id) : IRequest<GetServicioByIdResponse>
{
    
}