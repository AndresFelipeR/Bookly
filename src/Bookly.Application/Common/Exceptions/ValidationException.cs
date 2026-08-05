namespace Bookly.Application.Common.Exceptions;

public sealed class ValidationException : Exception
{
    public IReadOnlyList<Error> Errors { get; }

    public ValidationException(IReadOnlyList<Error> errors) :
        base("Se produjeron uno o más errores de validación.")

    {
        Errors = errors;
    }
}