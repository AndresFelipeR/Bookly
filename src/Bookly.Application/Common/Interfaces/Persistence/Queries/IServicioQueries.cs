using Bookly.Application.Servicios.Queries.GetById;

namespace Bookly.Application.Common.Interfaces.Persistence.Queries;

public interface IServicioQueries
{
    Task<GetServicioByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
