namespace Bookly.Application.Common;

public sealed class Result<T>
{
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    //public Error Error { get; }
    public IReadOnlyList<Error> Errors { get; }

    public static Result<T> Success(T value) => new(true, value, Array.Empty<Error>());
    public static Result<T> Failure(Error error) => new(false, default, new [] { error });
    public static Result<T> Failure(IEnumerable<Error> errors) => new (false,default, errors.ToList());

    private Result(bool isSuccess, T? value, IReadOnlyList<Error> errors)
    {
        if(isSuccess && errors.Count > 0)
        {
            throw new InvalidOperationException("Un resultado exitoso no puede tener un error.");
        }
        if(!isSuccess && errors.Count == 0)
        {
            throw new InvalidOperationException("Un resultado fallido debe tener un error.");
        }
        (IsSuccess, Value, Errors) = (isSuccess, value, errors);
    }
}