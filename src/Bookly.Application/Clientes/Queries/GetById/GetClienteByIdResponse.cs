namespace Bookly.Application.Clientes.Queries.GetById;

public sealed record GetClienteByIdResponse(
    Guid Id,
    string Nombre,
    string Apellido,
    string Correo,
    string Telefono);