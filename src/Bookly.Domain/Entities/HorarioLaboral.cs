using Bookly.Domain.Common;
using Bookly.Domain.Exceptions;

namespace Bookly.Domain.Entities;

public sealed class HorarioLaboral : BaseEntity
{
    public string Nombre { get; private set; } = null!;
    public string Descripcion { get; private set; } = null!;

    private readonly List<HorarioLaboralDetalle> _detalles = [];
    public IReadOnlyCollection<HorarioLaboralDetalle> Detalles => _detalles;

    private HorarioLaboral()
    {
        // Constructor privado para EF Core
    }

    private HorarioLaboral(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }

    public static HorarioLaboral Create(
        string nombre,
        string descripcion,
        IEnumerable<HorarioLaboralDetalle> detalles)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del horario laboral no puede estar vacío.");

        ArgumentNullException.ThrowIfNull(detalles);

        var detalleList = detalles.ToList();
        if (detalleList.Count == 0)
            throw new DomainException("El horario laboral debe tener al menos un detalle.");

        ValidarSolapes(detalleList);

        var horario = new HorarioLaboral(nombre.Trim(), descripcion?.Trim() ?? string.Empty);
        horario._detalles.AddRange(detalleList);
        return horario;
    }

    public void Activar()
    {
        if (State)
            return;

        State = true;
    }

    public void Desactivar()
    {
        if (!State)
            return;

        State = false;
    }

    private static void ValidarSolapes(IReadOnlyList<HorarioLaboralDetalle> detalles)
    {
        var ordenados = detalles
            .OrderBy(d => d.Dia)
            .ThenBy(d => d.HoraInicio)
            .ToList();

        for (var i = 0; i < ordenados.Count - 1; i++)
        {
            var actual = ordenados[i];
            var siguiente = ordenados[i + 1];

            if (actual.SeSolapaCon(siguiente))
                throw new DomainException($"El horario de {actual.Dia} se solapa con otro rango del mismo día.");
        }
    }
}
