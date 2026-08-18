using Bookly.Domain.Entities;

namespace Bookly.Application.Clientes.Commands.Create;

public sealed record CreateClienteResponse(
    Guid Id,
    String Nombre,
    String Apellido,
    string Telefono,
    string Email
)
{
    public static CreateClienteResponse FromCliente(Cliente cliente)
    {
        return new CreateClienteResponse(
            cliente.Id,
            cliente.NombreCompleto.Nombre,
            cliente.NombreCompleto.Apellido,
            cliente.Telefono.Value,
            cliente.Email.Value);
    }
}