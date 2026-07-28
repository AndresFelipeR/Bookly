namespace Bookly.Application.Common;

public sealed class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    private Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Un resultado exitoso no puede tener un error.");
        }
        if(!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Un resultado fallido debe tener un error.");
        }
        IsSuccess = isSuccess;
        Error = error;
    }
}