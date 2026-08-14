using Bookly.Application.Clientes.Queries.GetById;
using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Queries;

public class ClienteQueries : IClienteQueries
{
    private readonly BooklyDbContext _dbContext;

    public ClienteQueries(BooklyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetClienteByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Clientes
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(x => new GetClienteByIdResponse(
                x.Id,
                x.NombreCompleto.Nombre,
                x.NombreCompleto.Apellido,
                x.Email.Value,
                x.Telefono.Value))
            .FirstOrDefaultAsync(cancellationToken);
    }
}