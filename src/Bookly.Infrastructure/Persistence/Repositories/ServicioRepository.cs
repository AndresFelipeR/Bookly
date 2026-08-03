using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Repositories;

public sealed class ServicioRepository : BaseRepository<Servicio>, IServicioRepository
{
   
    public ServicioRepository(BooklyDbContext context) : base(context) 
    {
        
    }

    public async Task<Servicio?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Servicios
            .Include(x => x.TipoServicio)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    

    public Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken)
    {
       Context.Servicios.Update(servicio);
       return Task.CompletedTask;
    }

}