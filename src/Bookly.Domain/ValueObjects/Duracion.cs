using Blookly.Domain.Common;

namespace Blookly.Domain.ValueObjects;

public sealed class Duracion : ValueObject
{

    public TimeSpan Value { get;}
    public static Duracion FromMinutes(int minutes)
    {
        return new Duracion(TimeSpan.FromMinutes(minutes));
    }
    public static Duracion FromHours(int hours)
    {
        return new Duracion(TimeSpan.FromHours(hours));
    }

    public static Duracion FromTimeSpan(TimeSpan timeSpan)
    {
       return new Duracion(timeSpan);
    }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private Duracion(TimeSpan value)
    {
        if (value <= TimeSpan.Zero)
            throw new ArgumentException("La duración debe ser mayor a cero.", nameof(value));

        Value = value;
    }

    public int TotalMinutes => (int)Value.TotalMinutes;
    public double TotalHours => Value.TotalHours;

    public Duracion Sumar(Duracion other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return new Duracion(Value + other.Value);
    }

    public Duracion Restar(Duracion other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var result = Value - other.Value;
        if (result <= TimeSpan.Zero)
            throw new InvalidOperationException("La duración resultante no puede ser menor o igual a cero.");

        return new Duracion(result);
    }
}
