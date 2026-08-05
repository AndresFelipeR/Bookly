namespace Bookly.Application.Common;

public sealed class Result
{
    public bool IsSuccess { get; }
    public IReadOnlyList<Error> Errors { get; }
    public bool IsFailure => !IsSuccess;
    //public Error Error { get; }

    public static Result Success() => new(true, Array.Empty<Error>());
    public static Result Failure(Error error) => new(false, new [] {error});
    public static Result Failure(IEnumerable<Error> errors) => new(false, errors.ToList());

    private Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        if(isSuccess && errors.Count > 0)
        {
            throw new InvalidOperationException("Un resultado exitoso no puede tener un error.");
        }
        if(!isSuccess && errors.Count == 0)
        {
            throw new InvalidOperationException("Un resultado fallido debe tener un error.");
        }
        IsSuccess = isSuccess;
        Errors = errors;
    }
}