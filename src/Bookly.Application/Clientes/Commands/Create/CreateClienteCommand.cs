using MediatR;

namespace Bookly.Application.Clientes.Commands.Create;

public sealed record CreateClienteCommand
(
    string Nombre,
    string Apellido,
    string Email,
    string Telefono
): IRequest<CreateClienteResponse>;