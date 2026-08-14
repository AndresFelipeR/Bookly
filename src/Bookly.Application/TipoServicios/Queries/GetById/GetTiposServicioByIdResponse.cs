namespace Bookly.Application.TipoServicios.Queries.GetById;

public sealed record GetTiposServicioByIdResponse
    (
    Guid Id,
    string Nombre,
    string Descripcion
    );