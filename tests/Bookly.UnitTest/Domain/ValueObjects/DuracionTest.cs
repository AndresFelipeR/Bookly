using Bookly.Domain.ValueObjects;

namespace Bookly.UnitTest.Domain.ValueObjects;

public class DuracionTest
{
    [Fact]
    public void FromMinutes_CreaDuracionCorrectamente()
    {
        //Arrange
        const int min = 60;
        //Act
        Duracion dur = Duracion.FromMinutes(min);
        //Assert
        Assert.Equal(60,dur.TotalMinutes);

    }

    [Fact]
    public void FromMinutes_Cero_LanzaArgumentException()
    {
        const int min = 0;
        Action action = () => Duracion.FromMinutes(min);
        Assert.Throws<ArgumentException>(action);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void FromMinutes_ValorNoValido_LanzaArgumentException(int min)
    {
        Action action = () => Duracion.FromMinutes(min);
        Assert.Throws<ArgumentException>(action);
    }

    [Theory]
    [InlineData(1,60)]
    [InlineData(2,120)]
    [InlineData(3,180)]
    public void FromHours_CrearDuracionCorrectamente(int hours,int expmin)
    {
        Duracion dur = Duracion.FromHours(hours);
        Assert.Equal(expmin, dur.TotalMinutes);
    }

    [Theory]
    [InlineData(1.5, 90)]
    [InlineData(2, 120)]
    [InlineData(3.5, 210)]
    public void FromTimeSpan_CrearDuracionCorrectamente(double hours, double expMin)
    {
        TimeSpan time = TimeSpan.FromHours(hours);
        Duracion dur = Duracion.FromTimeSpan(time);
        Assert.Equal(expMin, dur.TotalMinutes);
    }

    [Fact]
    public void FromTimeSpan_DuracionConSegundos()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(90);
        Duracion dur = Duracion.FromTimeSpan(timeSpan);
        Assert.Equal(1, dur.TotalMinutes);
    }

    [Theory]
    [InlineData(20,30,50)]
    [InlineData(30,40,70)]
    [InlineData(40,50,90)]
    public void SumarDuracionValido(int v1,int v2,int exp)
    {
        Duracion dur1 = Duracion.FromMinutes(v1);
        Duracion dur2 = Duracion.FromMinutes(v2);
        
        Duracion dur3 = dur1.Sumar(dur2);
        
        Assert.Equal(exp, dur3.TotalMinutes);
        
    }
    [Fact]
    public void SumarDuracionOverflow()
    {
        Duracion dur1 = Duracion.FromTimeSpan(TimeSpan.MaxValue);

        Action act = () => dur1.Sumar(dur1);
        Assert.Throws<OverflowException>(act);
        // ¿Qué excepción ocurre?
    }

    [Fact]
    public void Sumar_CuandoDuracionEsNull_LanzaArgumentNullException()
    {
        Duracion dur1 = Duracion.FromMinutes(50);
        Duracion? dur2 = null;
        Action act = () => dur1.Sumar(dur2);
        Assert.Throws<ArgumentNullException>(act);
    }
}