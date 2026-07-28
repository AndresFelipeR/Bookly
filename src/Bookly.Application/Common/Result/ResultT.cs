namespace Bookly.Application.Common;

public sealed class Result<T>
{
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public Error Error { get; }

    public static Result<T> Success(T value) => new(true, value, Error.None);
    public static Result<T> Failure(Error error) => new(false, default, error);

    private Result(bool isSuccess, T? value, Error error)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("Un resultado exitoso no puede tener un error.");
        }
        if(!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("Un resultado fallido debe tener un error.");
        }
        (IsSuccess, Value, Error) = (isSuccess, value, error);
    }
}