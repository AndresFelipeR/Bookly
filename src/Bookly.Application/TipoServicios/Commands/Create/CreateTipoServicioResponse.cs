

using Bookly.Domain.Entities;

namespace Bookly.Application.TipoServicios.Commands.Create;


public sealed record CreateTipoServicioResponse(
    Guid Id,
    string Nombre,
    string Descripcion
)
{
    public static CreateTipoServicioResponse FromTipoServicio(TipoServicio  tipoServicio)
    {
        return new CreateTipoServicioResponse(
            tipoServicio.Id,
            tipoServicio.Nombre,
            tipoServicio.Descripcion
        );
    }
}