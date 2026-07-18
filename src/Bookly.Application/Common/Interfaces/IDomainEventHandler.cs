using Blookly.Domain.Common;

namespace Blookly.Application.Common.Interfaces;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
