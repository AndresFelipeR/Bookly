using Blookly.Application.Common.Interfaces;
using Blookly.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Blookly.Application.Common.Handlers;

public class ServicioCreadoDomainEventHandler : IDomainEventHandler<ServicioCreadoDomainEvent>
{
   
    public ServicioCreadoDomainEventHandler()
    {
      
    }

    public Task Handle(ServicioCreadoDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Servicio creado {domainEvent.ServicioId}");
        return Task.CompletedTask;
    }
}
