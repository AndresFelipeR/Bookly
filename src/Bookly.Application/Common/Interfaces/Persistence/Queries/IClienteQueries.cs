using Bookly.Application.Clientes.Queries.GetById;

namespace Bookly.Application.Common.Interfaces.Persistence.Queries;

public interface IClienteQueries
{
    Task<GetClienteByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}