using Bookly.Domain.Entities;

namespace Bookly.Application.Empleados.Commands.Create;

public sealed record CreateEmpleadoResponse(
    Guid Id,
    string Nombre,
    string Apellido,
    string Telefono,
    string Email)
{
    public static CreateEmpleadoResponse FromEmpleado(Empleado empleado)
    {
        return new CreateEmpleadoResponse(
            empleado.Id,
            empleado.NombreCompleto.Nombre,
            empleado.NombreCompleto.Apellido,
            empleado.Telefono.Value,
            empleado.Email.Value);
    }
}
