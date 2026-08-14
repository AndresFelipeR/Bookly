using System.Net.Mail;
using Bookly.Domain.Common;

namespace Bookly.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
    private Email(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
         throw new ArgumentException("El Email no puede estar vacio");

       bool esValido = MailAddress.TryCreate(value, out var _);
        if(!esValido)
            throw new ArgumentException("El Email no es valido");

        Value = value.ToLowerInvariant();
    }

    public static Email Create(string email)
    {
        return new Email(email);
    }
}
