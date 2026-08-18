using Bookly.Application.HorariosLaborales.Queries.GetById;
using MediatR;

namespace Bookly.Application.Empleados.Queries.GetHorariosLaborales;

public sealed record GetEmpleadoHorariosLaboralesQuery(Guid EmpleadoId)
    : IRequest<IReadOnlyList<GetHorarioLaboralByIdResponse>>;
