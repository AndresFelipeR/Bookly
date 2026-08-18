using MediatR;

namespace Bookly.Application.Empleados.Commands.AsignarServicio;

public sealed record AsignarServicioEmpleadoCommand(
    Guid EmpleadoId,
    Guid ServicioId) : IRequest<AsignarServicioEmpleadoResponse>;
