using Blookly.Domain.Common;
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

    private Servicio(string nombre, string descripcion, Money precio, TipoServicio tipoServicio, Duracion duracion)
    {
        ArgumentNullException.ThrowIfNull(nombre);
        ArgumentNullException.ThrowIfNull(descripcion);
        ArgumentNullException.ThrowIfNull(tipoServicio);
        ArgumentNullException.ThrowIfNull(precio);
        ArgumentNullException.ThrowIfNull(duracion);

        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        TipoServicio = tipoServicio;
        Duracion = duracion;
        TipoServicioId = tipoServicio.Id;
    }

    protected Servicio()
    {
        // Constructor protegido para EF Core
    }

    public static Servicio Create(string nombre, string descripcion, Money precio, TipoServicio tipoServicio, Duracion duracion)
    {
        return new Servicio(nombre, descripcion, precio, tipoServicio, duracion);
    }
    
}
