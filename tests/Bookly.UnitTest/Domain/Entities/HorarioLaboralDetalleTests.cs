using Bookly.Domain.Entities;
using Bookly.Domain.Enums;
using Bookly.Domain.Exceptions;

namespace Bookly.UnitTest.Domain.Entities;

public class HorarioLaboralDetalleTests
{
    [Fact]
    public void Create_CuandoDatosValidos_CreaDetalle()
    {
       DiaSemana diaSemana = DiaSemana.Lunes;
       TimeOnly horaInicio = new(9, 0);
       TimeOnly horaFin = new(13, 0);
       
       var create = HorarioLaboralDetalle.Create(diaSemana, horaInicio, horaFin);
       Assert.Equal(diaSemana, create.Dia);
       Assert.Equal(horaInicio, create.HoraInicio);
       Assert.Equal(horaFin, create.HoraFin);
    }

    [Theory]
    [InlineData(9,9)]
    [InlineData(10,9)]
    public void Create_CuandoHoraInicioNoEsAnteriorALaHoraFin_LanzaDomainException(int HoraInicio, int HoraFin)
    {
        DiaSemana dia = DiaSemana.Lunes;
        TimeOnly horaInicio = new(HoraInicio, 0);
        TimeOnly horaFin = new(HoraFin, 0);
        Action act = () => HorarioLaboralDetalle.Create(dia, horaInicio, horaFin);
        Assert.Throws<DomainException>(act);
    }

    [Fact]
    public void SeSolapaCon_CuandoMismoDiaYLosHorariosSeSolapan_DevuelveTrue()
    {
        HorarioLaboralDetalle h1 =  HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(9,0),new(13,0));
        HorarioLaboralDetalle h2 =  HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(11,0),new(15,0));
        
        
        Assert.True(h1.SeSolapaCon(h2));
    }

    [Fact]
    public void NoSeSolapan_MismoDiaHorarioCOnsecutivos_DevuelveFalse()
    {
        DiaSemana dia = DiaSemana.Lunes;
        HorarioLaboralDetalle h1 =  HorarioLaboralDetalle.Create(dia,new(9,0),new(13,0));
        HorarioLaboralDetalle h2 =  HorarioLaboralDetalle.Create(dia,new(13,0),new(17,0));
        Assert.False(h1.SeSolapaCon(h2));
        
    }

    [Fact]
    public void NoSeSolapan_DiaDiferentes_DevuelveFalse()
    {
        HorarioLaboralDetalle h1 = HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(9,0),new(13,0));
        HorarioLaboralDetalle h2 = HorarioLaboralDetalle.Create(DiaSemana.Martes,new(10,0),new(12,0));
        Assert.False(h1.SeSolapaCon(h2));
    }

    [Fact]
    public void CuandoSeEnviaNull()
    {
        HorarioLaboralDetalle h1 = HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(9,0),new(13,0));
        HorarioLaboralDetalle? h2 = null;
        Action act = () => h1.SeSolapaCon(h2);
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void MismoDiaYHorario_DevuelveTrue()
    {
        HorarioLaboralDetalle h1 = HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(9,0),new(13,0));
        HorarioLaboralDetalle h2 = HorarioLaboralDetalle.Create(DiaSemana.Lunes,new(9,0),new(13,0));
        Assert.True(h1.SeSolapaCon(h2));
    }
    
}