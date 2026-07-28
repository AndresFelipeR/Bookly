using Bookly.Domain.Entities;

namespace Bookly.Application.Servicios.Commands.Create;

public sealed record CreateServicioResponse(
    Guid Id,
    string Nombre,
    string Descripcion,
    decimal Amount,
    string Currency,
    Guid TipoServicioId,
    int Duracion,
    int MargenCancelacion,
    int MargenAnticipacion)
{
    public static CreateServicioResponse FromServicio(Servicio servicio)
    {
        return new CreateServicioResponse(
            servicio.Id,
            servicio.Nombre,
            servicio.Descripcion,
            servicio.Precio.Amount,
            servicio.Precio.Currency,
            servicio.TipoServicioId,
            servicio.Duracion.TotalMinutes,
            servicio.PoliticaReserva.MargenCancelacion.TotalMinutes,
            servicio.PoliticaReserva.MargenAnticipacion.TotalMinutes);
    }
}