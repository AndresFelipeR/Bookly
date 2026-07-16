using Blookly.Domain.Common;

namespace Blookly.Domain.ValueObjects;

public sealed class FullName : ValueObject
{
    public string Nombre { get; }
    public string Apellido { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Nombre;
        yield return Apellido;
    }

    public FullName(string nombre, string apellido)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(apellido))
            throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));

        Nombre = nombre;
        Apellido = apellido;
    }
}
