using Bookly.Application.Common;

namespace Bookly.Application.Common.Exceptions;

public sealed class ConflictException : Exception
{
    public Error Error { get; }

    public ConflictException(Error error) : base(error.Message)
    {
        Error = error;
    }
}