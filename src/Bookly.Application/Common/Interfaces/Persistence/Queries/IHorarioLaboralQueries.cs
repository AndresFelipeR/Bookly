using Bookly.Application.HorariosLaborales.Queries.GetById;

namespace Bookly.Application.Common.Interfaces.Persistence.Queries;

public interface IHorarioLaboralQueries
{
    Task<GetHorarioLaboralByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
