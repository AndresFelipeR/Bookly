using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;

namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(BooklyDbContext context) : base(context)
    {
    }
}