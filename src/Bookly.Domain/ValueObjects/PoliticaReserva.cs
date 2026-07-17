using Blookly.Domain.Common;

namespace Blookly.Domain.ValueObjects;

public sealed class PoliticaReserva : ValueObject
{

    public Duracion MargenCancelacion { get; private set; }
    public Duracion MargenAnticipacion { get; private set; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return MargenCancelacion;
        yield return MargenAnticipacion;
    }

    public PoliticaReserva(Duracion margenCancelacion, Duracion margenAnticipacion)
    {
        ArgumentNullException.ThrowIfNull(margenCancelacion);
        ArgumentNullException.ThrowIfNull(margenAnticipacion);


        if (margenCancelacion.Value > margenAnticipacion.Value)
            throw new ArgumentException("El margen de cancelación no puede ser mayor que el margen de anticipación.");

        
        MargenCancelacion = margenCancelacion;
        MargenAnticipacion = margenAnticipacion;
    }

    private PoliticaReserva()
    {
        // Constructor privado para EF Core
    }

    public static PoliticaReserva Create(Duracion margenCancelacion, Duracion margenAnticipacion)
    {
        return new PoliticaReserva(margenCancelacion, margenAnticipacion);
    }
}
