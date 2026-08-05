using Bookly.Application.Common;

namespace Bookly.Application.Common.Exceptions;

public sealed class NotFoundException : Exception
{
    public Error Error { get; }

    public NotFoundException(Error error)
        : base(error.Message)
    {
        Error = error;
    }
}
