

using Bookly.Application.Common;
using MediatR;

namespace Bookly.Application.Servicios.Commands.Create;

public sealed record CreateServicioCommand(

    string Nombre,
    string Descripcion,
    decimal Amount,
    string Currency,
    Guid TipoServicioId,
    int Duracion,
    int MargenCancelacion,
    int MargenAnticipacion
) : IRequest<CreateServicioResponse>;