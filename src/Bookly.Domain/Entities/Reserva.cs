using Bookly.Domain.Common;
using Bookly.Domain.Enums;
using Bookly.Domain.ValueObjects;

namespace Bookly.Domain.Entities;

public sealed class Reserva : BaseEntity
{
    private readonly List<ReservaServicio> _servicios = [];
     public EstadoReserva Estado { get; private set; } // esto podria ser un enumerado o un value object dependiendo de la complejidad que queramos darle
     public DateTime FechaReserva { get; private set; }
     public Cliente Cliente { get; private set; }
     public Guid ClienteId { get; private set; } // esto es para EF Core, para que pueda mapear la relación con Cliente
     
     public IReadOnlyCollection<ReservaServicio> Servicios => _servicios.AsReadOnly();
     
     public Money PrecioTotal
     {
         get
         {
             return _servicios
                 .Select(x => x.Precio)
                 .Aggregate(
                     (total,precio) => total.Add(precio));
         }
     }

     public Duracion DuracionTotal
     {
         get
         {
             var total = _servicios[0].Duracion;
             for (var i = 1; i < _servicios.Count; i++)
             {
                 total = total.Sumar(_servicios[i].Duracion);
             }
             return total;
         }
     }

    private Reserva( DateTime fechaReserva, Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);

        Estado = EstadoReserva.Pendiente; // por defecto la reserva se crea en estado pendiente
        FechaReserva = fechaReserva;
        Cliente = cliente;
        ClienteId = cliente.Id;
    }

  
    private Reserva()
    {
        // Constructor protegido para EF Core
    }

    public static Reserva Create(DateTime fechaReserva, Cliente cliente)
    {
        return new Reserva(fechaReserva, cliente);
    }

    public void AgregarServicio(ReservaServicio servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        if(_servicios.Any(x => x.ServicioId == servicio.ServicioId))
            throw new ArgumentException("La servicio ya existe en la reserva.");
        _servicios.Add(servicio);
    }

    public void QuitarServicio(Guid servicioId)
    {
        var servicio = _servicios.FirstOrDefault(x => x.ServicioId == servicioId);
        
        if(servicio is null)
            throw new InvalidOperationException("La servicio no existe en la reserva.");
        _servicios.Remove(servicio);
    }

    public void Cancelar()
    {
        if(Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("La reserva ya está cancelada.");
        
        if(Estado == EstadoReserva.Pagada)
            throw new InvalidOperationException("No se puede cancelar una reserva pagada.");
        if(Estado == EstadoReserva.Completada)
            throw new InvalidOperationException("No se puede cancelar una reserva completada.");
        if(Estado == EstadoReserva.Confirmada)
            throw new InvalidOperationException("No se puede cancelar una reserva confirmada.");

        Estado = EstadoReserva.Cancelada;
    }

    public void Confirmar()
    {
        if(Estado == EstadoReserva.Confirmada)
            throw new InvalidOperationException("La reserva ya está confirmada.");
        
        if(Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("No se puede confirmar una reserva cancelada.");
        
        if(Estado == EstadoReserva.Pagada)
            throw new InvalidOperationException("No se puede confirmar una reserva pagada.");
        if(Estado == EstadoReserva.Completada)
            throw new InvalidOperationException("No se puede confirmar una reserva completada.");
        if(Estado == EstadoReserva.Pendiente)
        {
            Estado = EstadoReserva.Confirmada;
        }
    }

    public void CambiarFechaReserva(DateTime nuevaFechaReserva)
    {
        if(nuevaFechaReserva == FechaReserva)
            return;
        if(nuevaFechaReserva < DateTime.UtcNow)
            throw new ArgumentException("La nueva fecha de reserva no puede ser anterior a la fecha actual.");

        FechaReserva = nuevaFechaReserva;
    }

 public void Pagar()
    {
        if(Estado == EstadoReserva.Pagada)
            throw new InvalidOperationException("La reserva ya está pagada.");

        if(Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("No se puede pagar una reserva cancelada.");

        if(Estado != EstadoReserva.Confirmada)
            throw new InvalidOperationException("Solo se puede pagar una reserva confirmada.");

        Estado = EstadoReserva.Pagada;
    }

    

}
