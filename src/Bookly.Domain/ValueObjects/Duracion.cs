using Blookly.Domain.Common;

namespace Blookly.Domain.ValueObjects;

public sealed class Duracion : ValueObject
{

    public int Minutos { get; private set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Minutos;
    }

    public Duracion(int minutos)
    {
        if (minutos <= 0)
            throw new ArgumentException("La duración debe ser mayor a cero.", nameof(minutos));

        Minutos = minutos;
    }
}
