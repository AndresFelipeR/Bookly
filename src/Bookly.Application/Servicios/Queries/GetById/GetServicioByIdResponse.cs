namespace Bookly.Application.Servicios.Queries.GetById;

public sealed record GetServicioByIdResponse(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    string Currency,
    string TipoServicio,
    int Duracion,
    int MargenCancelacion,
    int MargenAnticipacion);
