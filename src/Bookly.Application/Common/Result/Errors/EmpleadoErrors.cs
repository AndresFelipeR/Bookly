namespace Bookly.Application.Common.Errors;

public static class EmpleadoErrors
{
    public static Error NotFound(Guid id) => new(
        "Empleado.NotFound", $"El empleado con el id {id} no existe.");

    public static Error ServicioYaAsignado(Guid empleadoId, Guid servicioId) => new(
        "Empleado.ServicioYaAsignado",
        $"El servicio {servicioId} ya está asignado al empleado {empleadoId}.");

    public static Error ServicioNoAsignado(Guid empleadoId, Guid servicioId) => new(
        "Empleado.ServicioNoAsignado",
        $"El servicio {servicioId} no está asignado al empleado {empleadoId}.");

    public static Error HorarioLaboralYaAsignado(Guid empleadoId, Guid horarioLaboralId) => new(
        "Empleado.HorarioLaboralYaAsignado",
        $"El horario laboral {horarioLaboralId} ya está asignado al empleado {empleadoId}.");

    public static Error HorarioLaboralNoAsignado(Guid empleadoId, Guid horarioLaboralId) => new(
        "Empleado.HorarioLaboralNoAsignado",
        $"El horario laboral {horarioLaboralId} no está asignado al empleado {empleadoId}.");
}
