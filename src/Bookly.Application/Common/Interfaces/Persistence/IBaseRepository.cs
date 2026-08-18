using Bookly.Domain.Common;

namespace Bookly.Application.Common.Interfaces.Persistence;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
}