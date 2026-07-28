using Bookly.Application.Common.Interfaces;
using Bookly.Domain.Common;
using Bookly.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Bookly.Application.Common.Handlers;

public class DomainEventDispatcher : IDomainEventDispatcher
{

    public async Task Dispatchasync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach ( var domainEvent in domainEvents)
        {
            cancellationToken.ThrowIfCancellationRequested();
           if(domainEvent is ServicioCreadoDomainEvent evento)
            {
                var handler = new ServicioCreadoDomainEventHandler();
                await handler.Handle(evento,cancellationToken);
            }
            
        }
    }
}
