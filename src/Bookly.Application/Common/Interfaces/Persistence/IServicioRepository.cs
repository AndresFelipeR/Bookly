using Bookly.Domain.Entities;

namespace Bookly.Application.Common.Interfaces.Persistence;

public interface IServicioRepository : IBaseRepository<Servicio>
{
   // Task AddAsync(Servicio servicio, CancellationToken cancellationToken);
    //Task<Servicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    //Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken);
}