using Blookly.Domain.Common;
using Blookly.Domain.Events;
using Blookly.Domain.ValueObjects;

namespace Blookly.Domain.Entities;

public sealed class Servicio : BaseEntity
{

    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public Money Precio { get; private set; }
    public TipoServicio TipoServicio { get; private set; }
    public Duracion Duracion { get; private set; }
    public Guid TipoServicioId { get; private set; } // esto es para EF Core, para que pueda mapear la relación con TipoServicio
    public PoliticaReserva PoliticaReserva { get; private set; }

    private Servicio(string nombre, string descripcion, Money precio, TipoServicio tipoServicio, Duracion duracion, PoliticaReserva politicaReserva)
    {
        ArgumentNullException.ThrowIfNull(nombre);
        ArgumentNullException.ThrowIfNull(descripcion);
        ArgumentNullException.ThrowIfNull(tipoServicio);
        ArgumentNullException.ThrowIfNull(precio);
        ArgumentNullException.ThrowIfNull(duracion);
        ArgumentNullException.ThrowIfNull(politicaReserva);

        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        TipoServicio = tipoServicio;
        Duracion = duracion;
        TipoServicioId = tipoServicio.Id;
        PoliticaReserva = politicaReserva;
    }

    protected Servicio()
    {
        // Constructor protegido para EF Core
    }

    public static Servicio Create(string nombre, string descripcion, Money precio, TipoServicio tipoServicio, Duracion duracion, PoliticaReserva politicaReserva)
    {
         var servicio = new Servicio(nombre, descripcion, precio, tipoServicio, duracion, politicaReserva);
         servicio.AddDomainEvent( new ServicioCreadoDomainEvent(servicio.Id));

         return servicio;
    }

    public void CambiarDescripcion(string nuevaDescripcion)
    {
        if (string.IsNullOrWhiteSpace(nuevaDescripcion))
            throw new ArgumentException("La descripción no puede estar vacía.", nameof(nuevaDescripcion));
        if(nuevaDescripcion == Descripcion)
            return; // No hacer nada si la descripción es la misma
        Descripcion = nuevaDescripcion;
    }

public void CambiarNombre(string nuevoNombre)
    {
        if (string.IsNullOrWhiteSpace(nuevoNombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nuevoNombre));
        if(nuevoNombre == Nombre)
            return; // No hacer nada si el nombre es el mismo
        Nombre = nuevoNombre;
    }
    public void CambiarPrecio(Money nuevoPrecio)
    {
        ArgumentNullException.ThrowIfNull(nuevoPrecio);
          if (Precio.Equals(nuevoPrecio))
        return;

        var precioAnterior = Precio;
        Precio = nuevoPrecio;
       AddDomainEvent(
        new ServicioPrecioActualizadoDomainEvent(
            Id,
            precioAnterior,
            nuevoPrecio));
    }

    public void CambiarDuracion(Duracion nuevaDuracion)
    {
        ArgumentNullException.ThrowIfNull(nuevaDuracion);
        if(nuevaDuracion == Duracion)
            return; // No hacer nada si la duración es la misma
        Duracion = nuevaDuracion;
    }

    public void Activar()
    {
        if (State)
            throw new InvalidOperationException("El servicio ya está activo.");
        State = true;
    }

    public void Desactivar()
    {
        if (!State)
            throw new InvalidOperationException("El servicio ya está inactivo.");
        State = false;
    }

    public void CambiarTipoServicio(TipoServicio nuevoTipoServicio)
    {
        ArgumentNullException.ThrowIfNull(nuevoTipoServicio);

        if(TipoServicio.Equals(nuevoTipoServicio))
            throw new InvalidOperationException("El servicio ya pertenece a este tipo de servicio.");
        
        if(!nuevoTipoServicio.State)
            throw new InvalidOperationException("No se puede asignar un tipo de servicio inactivo.");
        

        TipoServicio = nuevoTipoServicio;
        TipoServicioId = nuevoTipoServicio.Id;
    }

    public void CambiarPoliticaReserva(PoliticaReserva nuevaPoliticaReserva)
    {
        ArgumentNullException.ThrowIfNull(nuevaPoliticaReserva);
        if(nuevaPoliticaReserva == PoliticaReserva)
            return; // No hacer nada si la política de reserva es la misma
        PoliticaReserva = nuevaPoliticaReserva;
    }
   
}
