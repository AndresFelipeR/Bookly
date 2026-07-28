using Bookly.Domain.Entities;

namespace Bookly.Application.Common.Interfaces.Persistence;

public interface IServicioRepository
{
    Task AddAsync(Servicio servicio, CancellationToken cancellationToken);
    Task<Servicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken);
    Task DeleteAsync(Servicio servicio, CancellationToken cancellationToken);
}