namespace Bookly.Application.Common.Errors;

public static class HorarioLaboralErrors
{
    public static Error NotFound(Guid id) => new(
        "HorarioLaboral.NotFound",
        $"El horario laboral con el id {id} no existe.");
}
