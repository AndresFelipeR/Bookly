using Bookly.Application.Common.Interfaces;
using Bookly.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Bookly.Application.Common.Handlers;

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
