using System.Net.Mail;

namespace Blookly.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }
    public Email(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
         throw new ArgumentException("El Email no puede estar vacio");

       bool esValido = MailAddress.TryCreate(value, out var _);
        if(!esValido)
            throw new ArgumentException("El Email no es valido");

        Value = value.ToLowerInvariant();
    }
}
