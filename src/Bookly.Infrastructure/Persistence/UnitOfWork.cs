using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Infrastructure.Persistence;

namespace Bookly.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BooklyDbContext _dbContext;

    public UnitOfWork(BooklyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}