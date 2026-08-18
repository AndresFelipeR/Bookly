namespace Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;

public sealed record AsignarHorarioLaboralEmpleadoResponse(
    Guid EmpleadoId,
    Guid HorarioLaboralId);
