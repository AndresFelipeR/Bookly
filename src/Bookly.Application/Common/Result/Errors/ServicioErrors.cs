namespace Bookly.Application.Common.Errors;

public static class ServicioErrors
{
    public static Error NotFound(Guid id) =>
        new(
            "Servicio.NotFound",
            $"El servicio con el id {id} no fue encontrado."
        );
}