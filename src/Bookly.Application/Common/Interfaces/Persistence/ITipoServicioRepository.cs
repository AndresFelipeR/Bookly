using Bookly.Domain.Entities;

namespace Bookly.Application.Common.Interfaces.Persistence;

public interface ITipoServicioRepository
{
    Task AddAsync(TipoServicio tipoServicioservicio, CancellationToken cancellationToken);
    Task<TipoServicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(TipoServicio tipoServicio, CancellationToken cancellationToken);
}