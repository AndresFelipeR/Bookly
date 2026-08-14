using Bookly.Application.TipoServicios.Queries.GetById;

namespace Bookly.Application.Common.Interfaces.Persistence.Queries;

public interface ITiposServicioQueries
{
    Task<GetTiposServicioByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}