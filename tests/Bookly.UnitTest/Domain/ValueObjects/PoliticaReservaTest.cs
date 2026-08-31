using Bookly.Domain.Exceptions;
using Bookly.Domain.ValueObjects;

namespace Bookly.UnitTest.Domain.ValueObjects;

public class PoliticaReservaTest
{
    [Fact]
    public void CrearPoliticaReservaValida()
    {
        //Arrange
        const int cancelacion = 60;
        const int anticipacion = 120;
        //Act
        Duracion cancelacionDur = Duracion.FromMinutes(cancelacion);
        Duracion anticipacionDur = Duracion.FromMinutes(anticipacion);
        
        PoliticaReserva politicaReserva = new(cancelacionDur, anticipacionDur);
        
        Assert.Equal(politicaReserva.MargenAnticipacion,anticipacionDur);
        Assert.Equal(politicaReserva.MargenCancelacion,cancelacionDur);
    }

    [Fact]
    public void CrearPoliticaReservaConAnticipacionMenorCancelacion_lanzaDomainException()
    {
        const int cancelacion = 60;
        const int anticipacion = 30;
        Duracion cancelacionDur = Duracion.FromMinutes(cancelacion);
        Duracion anticipacionDur = Duracion.FromMinutes(anticipacion);
        
        Action act = () => PoliticaReserva.Create(cancelacionDur, anticipacionDur);

        Assert.Throws<DomainException>(act);

    }

    [Fact]
    public void EnviarMargenNull_LanzaArgumentNullException()
    {
        Duracion cancelacionDur = Duracion.FromMinutes(60);
        Duracion? anticipacionDur = null;
        
        Action act = () => PoliticaReserva.Create(cancelacionDur, anticipacionDur);
        Assert.Throws<ArgumentNullException>(act);
    }
    [Fact]
    public void CrearPoliticaReserva_CuandoCancelacionEsNull_LanzaArgumentNullException()
    {
        // Arrange
        Duracion? cancelacion = null;
        var anticipacion = Duracion.FromMinutes(120);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            PoliticaReserva.Create(cancelacion, anticipacion));
    }
    [Fact]
    public void CrearPoliticaReserva_CuandoMargenesSonIguales_CreaCorrectamente()
    {
        // Arrange
        var margen = Duracion.FromMinutes(60);

        // Act
        var politica = PoliticaReserva.Create(margen, margen);

        // Assert
        Assert.Equal(margen, politica.MargenCancelacion);
        Assert.Equal(margen, politica.MargenAnticipacion);
    }
}