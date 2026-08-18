using MediatR;

namespace Bookly.Application.Empleados.Commands.QuitarHorarioLaboral;

public sealed record QuitarHorarioLaboralEmpleadoCommand(
    Guid EmpleadoId,
    Guid HorarioLaboralId) : IRequest;
