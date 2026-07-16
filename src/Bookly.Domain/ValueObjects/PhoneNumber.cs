using Blookly.Domain.Common;

namespace Blookly.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public PhoneNumber(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El numero de telefono no puede estar vacio");

        Value = value;
    }
}
