namespace Bookly.Application.Common.Errors;

public static class TipoServicioErrors
{
    public static Error NotFound(Guid id) =>
    new(
        "TipoServicio.NotFound",
        $"El tipo de servicio con el id {id} no fue encontrado."
    );
}