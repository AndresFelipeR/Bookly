using Blookly.Domain.Common;

namespace Blookly.Domain.Entities;

public sealed class TipoServicio : BaseEntity

{
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    

    private TipoServicio(string nombre, string descripcion)
    {
        ArgumentNullException.ThrowIfNull(nombre);
        ArgumentNullException.ThrowIfNull(descripcion);
      
        Nombre = nombre;
        Descripcion = descripcion;
        
    }

    protected TipoServicio()
    {
        // Constructor protegido para EF Core
    }

    public static TipoServicio Create(string nombre, string descripcion )
    {
        return new TipoServicio(nombre, descripcion);
    }

    public void Activar()
    {
        if (State)
            throw new InvalidOperationException("El tipo de servicio ya está activo.");
        State = true;
    }
    public void Desactivar()
    {
        if (!State)
            throw new InvalidOperationException("El tipo de servicio ya está inactivo.");
        State = false;
    }

    public void CambiarDescripcion(string nuevaDescripcion)
    {
        ArgumentNullException.ThrowIfNull(nuevaDescripcion);
        Descripcion = nuevaDescripcion;
    }
}
