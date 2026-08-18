using MediatR;

namespace Bookly.Application.Empleados.Commands.Create;

public sealed record CreateEmpleadoCommand
(
    string Nombre,
    string Apellido,
    string Email,
    string Telefono
) : IRequest<CreateEmpleadoResponse>;
