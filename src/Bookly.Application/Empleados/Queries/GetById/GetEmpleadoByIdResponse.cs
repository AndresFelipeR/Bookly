namespace Bookly.Application.Empleados.Queries.GetById;

public sealed record GetEmpleadoByIdResponse(
    Guid Id,
    string Nombre,
    string Apellido,
    string Correo,
    string Telefono);
