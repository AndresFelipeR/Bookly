using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly BooklyDbContext Context;
    

    public BaseRepository(BooklyDbContext context)
    {
        Context = context;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().ToListAsync();
    }
}