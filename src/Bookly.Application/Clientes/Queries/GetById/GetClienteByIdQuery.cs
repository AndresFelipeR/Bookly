using MediatR;

namespace Bookly.Application.Clientes.Queries.GetById;

public sealed record GetClienteByIdQuery(Guid Id) : IRequest<GetClienteByIdResponse>
{
    
}