using Blookly.Domain.Common;
using Blookly.Domain.Enums;

namespace Blookly.Domain.Entities;

public sealed class Reserva : BaseEntity
{

     public EstadoReserva Estado { get; private set; } // esto podria ser un enumerado o un value object dependiendo de la complejidad que queramos darle
     public DateTime FechaReserva { get; private set; }
     public Cliente Cliente { get; private set; }
     public Guid ClienteId { get; private set; } // esto es para EF Core, para que pueda mapear la relación con Cliente


    private Reserva( DateTime fechaReserva, Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);

        Estado = EstadoReserva.Pendiente; // por defecto la reserva se crea en estado pendiente
        FechaReserva = fechaReserva;
        Cliente = cliente;
        ClienteId = cliente.Id;
    }
    protected Reserva()
    {
        // Constructor protegido para EF Core
    }

    public static Reserva Create(DateTime fechaReserva, Cliente cliente)
    {
        return new Reserva(fechaReserva, cliente);
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
