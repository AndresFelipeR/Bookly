using Bookly.Domain.Common;
using Bookly.Domain.ValueObjects;

namespace Bookly.Domain.Entities;

public sealed class ReservaServicio : BaseEntity
{
    public Guid ServicioId { get; private set; }
    public string Nombre { get; private set; }
    public Money Precio { get; private set; }
    public Duracion Duracion { get; private set; }
    
    private  ReservaServicio()
    {
    }

    private ReservaServicio(Guid servicioId,string nombre, Money precio, Duracion duracion)
    {
        if(servicioId == Guid.Empty)
            throw new ArgumentException("El servicio es obligatorio.",nameof(servicioId));
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentNullException(nameof(nombre));
        
        ServicioId = servicioId;
        Nombre = nombre;
        Precio = precio;
        Duracion = duracion;
    }

    public static ReservaServicio Create(Guid servicioId,string nombre, Money precio, Duracion duracion)
    {
        return new ReservaServicio(servicioId, nombre, precio, duracion);
    }
    
    
}