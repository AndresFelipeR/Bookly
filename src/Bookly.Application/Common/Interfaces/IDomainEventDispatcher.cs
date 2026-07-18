using Blookly.Domain.Common;

namespace Blookly.Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task Dispatchasync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
