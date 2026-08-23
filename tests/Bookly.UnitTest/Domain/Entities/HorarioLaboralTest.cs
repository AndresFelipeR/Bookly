using Bookly.Domain.Entities;
using Bookly.Domain.Enums;
using Bookly.Domain.Exceptions;

namespace Bookly.UnitTest.Domain.Entities;

public class HorarioLaboralTest
{
    [Fact]
    public void CrearHorarioLaboralValido()
    {
        const string nombre = "Horario Mañana";
        const string Descripcion = "Descripcion del horario de mañana";
        var h1 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(9, 0),
            new(13, 0));
        var horario = HorarioLaboral.Create(
            nombre,
            Descripcion,
            new List<HorarioLaboralDetalle>{h1});
        
        Assert.Equal(nombre,horario.Nombre);
        Assert.Equal(Descripcion, horario.Descripcion);
        Assert.Single((horario.Detalles));
        Assert.Contains(h1, horario.Detalles);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void CrearHorarioLaboral_NombreInvalido_LanzaDomainException(string? nombre)
    {
        const string descripcion = "Descripcion del horario de mañana";
        var h1 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(9, 0),
            new(13, 0));
        Action act = () => HorarioLaboral.Create(
            nombre,
            descripcion,
            new List<HorarioLaboralDetalle>{h1});
        Assert.Throws<DomainException>(act);
        
    }
    
    [Fact]
    public void CrearHorarioLaboral_DetallesNull_LanzaArgumentNullException()
    {
        const string nombre = "Horario Mañana";
        const string descripcion = "Descripción válida";
        IEnumerable<HorarioLaboralDetalle>? detalles = null;
        Action act = () => HorarioLaboral.Create(
            nombre,
            descripcion,
            detalles);

        Assert.Throws<ArgumentNullException>(act);
    }
    [Fact]
    public void CrearHorarioLaboral_SinDetalles_LanzaDomainException()
    {
        const string nombre = "Horario Mañana";
        const string descripcion = "Descripción válida";

        var detalles = new List<HorarioLaboralDetalle>();

        Action act = () => HorarioLaboral.Create(
            nombre,
            descripcion,
            detalles);

        Assert.Throws<DomainException>(act);
    }

    private HorarioLaboral CrearHorarioValido()
    {
        const string nombre = "Horario Mañana";
        const string descripcion = "Descripción válida";

        var h1 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(9, 0),
            new(13, 0));

        var h2 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(13, 0),
            new(17, 0));

        var list = new List<HorarioLaboralDetalle> { h1, h2 };

        var horario = HorarioLaboral.Create(
            nombre,
            descripcion,
            list);

        return horario;
    }
    [Fact]
    public void CrearHorarioLaboral_ConDetallesSolapados_LanzaDomainException()
    {
        const string nombre = "Horario Mañana";
        const string descripcion = "Descripción válida";
        var h1 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(9, 0),
            new(13, 0));

        var h2 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(11, 0),
            new(15, 0));
        var list = new List<HorarioLaboralDetalle>{h1, h2};
        
        Action act = () => HorarioLaboral.Create(
            nombre,
            descripcion,
            list);
        Assert.Throws<DomainException>(act);
    }
    [Fact]
    public void CrearHorarioLaboral_ConDatosValidos()
    {
        const string nombre = "Horario Mañana";
        const string descripcion = "Descripción válida";

        var h1 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(9, 0),
            new(13, 0));

        var h2 = HorarioLaboralDetalle.Create(
            DiaSemana.Lunes,
            new(13, 0),
            new(17, 0));

        var list = new List<HorarioLaboralDetalle> { h1, h2 };

        var horario = HorarioLaboral.Create(
            nombre,
            descripcion,
            list);

        Assert.Equal(nombre, horario.Nombre);
        Assert.Equal(descripcion, horario.Descripcion);
        Assert.Equal(2, horario.Detalles.Count);
        Assert.Contains(h1, horario.Detalles);
        Assert.Contains(h2, horario.Detalles);
    }

    [Fact]
    public void Activar_CuandoEstaInactivo_CambiaEstadoATrue()
    {
        var horario = CrearHorarioValido();
        
        horario.Desactivar();
        horario.Activar();
        
        Assert.True(horario.State);
    }

    [Fact]
    public void Activar_CuandoYaEstaActivo_MantieneEstadoTrue()
    {
       var horario = CrearHorarioValido();
       
        horario.Activar();
        Assert.True(horario.State);
    }

    [Fact]
    public void Desactivar_CuandoEstaActivo_CambioEstadoFalse()
    {
        var horario = CrearHorarioValido();
        horario.Desactivar();
        Assert.False(horario.State);
    }

    [Fact]
    public void Desactivar_CuandoEstaDesactivado_MantieneEstadoFalse()
    {
        var horario = CrearHorarioValido();
        horario.Desactivar();
        horario.Desactivar();
        
        Assert.False(horario.State);
    }
}