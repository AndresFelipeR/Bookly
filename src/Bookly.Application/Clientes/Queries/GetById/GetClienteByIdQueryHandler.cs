using Bookly.Application.Common.Errors;
using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using MediatR;

namespace Bookly.Application.Clientes.Queries.GetById;

public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery,GetClienteByIdResponse>
{
    private readonly IClienteQueries _clienteQueries;

    public GetClienteByIdQueryHandler(IClienteQueries clienteQueries)
    {
        _clienteQueries = clienteQueries;
    }

    public async Task<GetClienteByIdResponse> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteQueries.GetByIdAsync(request.Id, cancellationToken);
        if (cliente is null)
            throw new NotFoundException(ClienteErrors.NotFound(request.Id));

        return cliente;
    }
}