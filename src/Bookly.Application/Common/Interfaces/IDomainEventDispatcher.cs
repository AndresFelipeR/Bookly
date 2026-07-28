using Bookly.Domain.Common;

namespace Bookly.Application.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task Dispatchasync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
