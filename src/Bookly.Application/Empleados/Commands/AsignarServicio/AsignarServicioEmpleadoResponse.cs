namespace Bookly.Application.Empleados.Commands.AsignarServicio;

public sealed record AsignarServicioEmpleadoResponse(
    Guid EmpleadoId,
    Guid ServicioId);
