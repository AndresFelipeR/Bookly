using Blookly.Domain.Common;
using Blookly.Domain.ValueObjects;

namespace Blookly.Domain.Events;

public sealed class ServicioPrecioActualizadoDomainEvent : IDomainEvent
{
   public Guid ServicioId { get; }
    public Money PrecioAnterior { get; }
    public Money NuevoPrecio { get; }
    public DateTime OccurredOn { get; }

   public ServicioPrecioActualizadoDomainEvent(
        Guid servicioId,
        Money precioAnterior,
        Money nuevoPrecio)
    {
        ServicioId = servicioId;
        PrecioAnterior = precioAnterior;
        NuevoPrecio = nuevoPrecio;
        OccurredOn = DateTime.UtcNow;
    }
}
