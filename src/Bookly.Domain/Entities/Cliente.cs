using Blookly.Domain.Common;
using Blookly.Domain.ValueObjects;

namespace Blookly.Domain.Entities;

public sealed class Cliente : BaseEntity
{
    public FullName NombreCompleto { get; private set;}
    public Email Email { get; private set;}
    public PhoneNumber Telefono {get; private set;}
    public bool Activo { get; private set;}

    protected Cliente()
    {
        // Constructor protegido para EF Core
    }

    private Cliente( FullName nombreCompleto, Email email, PhoneNumber telefono)
    {
       ArgumentNullException.ThrowIfNull(nombreCompleto);

       ArgumentNullException.ThrowIfNull(email);

       ArgumentNullException.ThrowIfNull(telefono);

        NombreCompleto = nombreCompleto;
        Email = email;
        Telefono = telefono;
        Activo = true;
    }

    public static Cliente Create(FullName nombreCompleto, Email email, PhoneNumber telefono)
    {
        return new Cliente(nombreCompleto, email, telefono);
    }

    public void Desactivar()
    {
        if(!Activo)
        return;

        Activo = false;
    }

    public void CambiarEmail(Email nuevoEmail)
    {
        if(nuevoEmail == null)
            throw new ArgumentNullException(nameof(nuevoEmail));

        if(Email.Equals(nuevoEmail))
            return;

        Email = nuevoEmail;
    }

    public void CambiarTelefono(string nuevoTelefono)
    {
        if(string.IsNullOrWhiteSpace(nuevoTelefono))
            throw new ArgumentException("El telefono no puede estar vacio");

        Telefono = new PhoneNumber(nuevoTelefono);
    }

    public void CambiarNombreCompleto(FullName nuevoNombreCompleto)
    {
        if(nuevoNombreCompleto == null)
            throw new ArgumentNullException(nameof(nuevoNombreCompleto));

        if(NombreCompleto.Equals(nuevoNombreCompleto))
            return;

        NombreCompleto = nuevoNombreCompleto;
    }    

    public void Activar()
    {
        if(Activo)
        return;

        Activo = true;
    }

}
