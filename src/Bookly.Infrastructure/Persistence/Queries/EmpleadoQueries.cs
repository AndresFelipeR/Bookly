using Bookly.Application.Common.Interfaces.Persistence.Queries;
using Bookly.Application.Empleados.Queries.GetById;
using Bookly.Application.Empleados.Queries.GetServicios;
using Bookly.Application.HorariosLaborales.Queries.GetById;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Infrastructure.Persistence.Queries;

public class EmpleadoQueries : IEmpleadoQueries
{
    private readonly BooklyDbContext _dbContext;

    public EmpleadoQueries(BooklyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetEmpleadoByIdResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Empleados
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Select(x => new GetEmpleadoByIdResponse(
                x.Id,
                x.NombreCompleto.Nombre,
                x.NombreCompleto.Apellido,
                x.Email.Value,
                x.Telefono.Value))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Empleados
            .AsNoTracking()
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GetEmpleadoServicioResponse>> GetServiciosAsync(
        Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Empleados
            .AsNoTracking()
            .Where(e => e.Id == empleadoId)
            .SelectMany(e => e.Servicios)
            .Select(s => new GetEmpleadoServicioResponse(
                s.Id,
                s.Nombre,
                s.Descripcion))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<GetHorarioLaboralByIdResponse>> GetHorariosLaboralesAsync(
        Guid empleadoId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Empleados
            .AsNoTracking()
            .Where(e => e.Id == empleadoId)
            .SelectMany(e => e.HorariosLaborales)
            .Select(x => x.HorarioLaboral)
            .Select(h => new GetHorarioLaboralByIdResponse(
                h.Id,
                h.Nombre,
                h.Descripcion,
                h.Detalles
                    .OrderBy(d => d.Dia)
                    .ThenBy(d => d.HoraInicio)
                    .Select(d => new GetHorarioLaboralDetalleResponse(
                        d.Id,
                        d.Dia,
                        d.HoraInicio,
                        d.HoraFin))
                    .ToList()))
            .ToListAsync(cancellationToken);
    }
}
