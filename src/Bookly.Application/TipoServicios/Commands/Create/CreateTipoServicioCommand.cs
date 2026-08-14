
using MediatR;

namespace Bookly.Application.TipoServicios.Commands.Create;

public sealed record CreateTipoServicioCommand
(
    string Nombre,
    string Descripcion 
) : IRequest<CreateTipoServicioResponse>;