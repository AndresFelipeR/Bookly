using Bookly.Domain.Common;

namespace Bookly.Domain.ValueObjects;

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
        
        if(!value.All(char.IsDigit))
            throw new ArgumentException("El numero de telefono solo debe contener digitos");

        Value = value;
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        return new PhoneNumber(phoneNumber);
    }
}
