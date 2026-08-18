using MediatR;

namespace Bookly.Application.Empleados.Commands.QuitarServicio;

public sealed record QuitarServicioEmpleadoCommand(
    Guid EmpleadoId,
    Guid ServicioId) : IRequest;
