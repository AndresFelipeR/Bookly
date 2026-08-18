using MediatR;

namespace Bookly.Application.Empleados.Commands.AsignarHorarioLaboral;

public sealed record AsignarHorarioLaboralEmpleadoCommand(
    Guid EmpleadoId,
    Guid HorarioLaboralId) : IRequest<AsignarHorarioLaboralEmpleadoResponse>;
