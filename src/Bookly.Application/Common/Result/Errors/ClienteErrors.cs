namespace Bookly.Application.Common.Errors;

public static class ClienteErrors
{
    public static Error NotFound(Guid id) => new(
        "Cliente.NotFound", $"El cliente con el id {id} no existe.");
}