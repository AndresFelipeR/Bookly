using MediatR;

namespace Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;

public sealed record AsignarHorarioLaboralEmpleadoCommand(
    Guid EmpleadoId,
    Guid HorarioLaboralId,
    DateOnly FechaInicio,
    DateOnly? FechaFin) : IRequest<AsignarHorarioLaboralEmpleadoResponse>;
