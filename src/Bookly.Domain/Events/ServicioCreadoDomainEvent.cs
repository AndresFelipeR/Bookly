using Blookly.Domain.Common;

namespace Blookly.Domain.Events;

public sealed class ServicioCreadoDomainEvent : IDomainEvent
{
    public DateTime OccurredOn {get;}
    public Guid ServicioId { get;}


    public ServicioCreadoDomainEvent(Guid servicioId)
    {
        ServicioId = servicioId;
        OccurredOn = DateTime.UtcNow;
    }
}
