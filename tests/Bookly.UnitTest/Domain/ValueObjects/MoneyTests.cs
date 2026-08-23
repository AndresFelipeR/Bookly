using Bookly.Domain.ValueObjects;

namespace Bookly.UnitTest.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void LanzarExceptionValorNegativo()
    {
        //Arrange - Preparacion de lo necesario 
        const decimal amount = -10;
        const string currency = "EUR";
        
        //Act - Ejecutar lo que quiero probar
        Action act = () => new Money(amount, currency);
        //Assert - Compruebo que ocurrio lo que esperaba
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void LanzarExceptionMonedaVacia()
    {
        //Arrange
        const decimal amount = 10;
        const string currency = "";
        
        //Act
        Action act = () => new Money(amount, currency);
        
        // Asset
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void NormalizarMonedaAMayusculas()
    {
        const decimal amount = 10;
        const string currency = "eur";
        
        var money = new Money(amount, currency);
        
        Assert.Equal(amount, money.Amount);
        Assert.Equal("EUR", money.Currency);
    }

    [Fact]
    public void AgregarMoney()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(50, "EUR");
        
        var money3 = money1.Add(money2);
        
        Assert.Equal(150, money3.Amount);
        Assert.Equal("EUR", money3.Currency);

        Assert.Equal(100, money1.Amount);
        Assert.Equal(50, money2.Amount);
    }
    [Fact]
    public void ArgumentosNullAdd()
    {
        Money money1 = new Money(100, "EUR");
        Money? money2 = null;

        Action act = () => money1.Add(money2);
        
        Assert.Throws<ArgumentNullException>(act);
    }
    [Fact]
    public void AgregarMoneyDiferenteMoneda()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(50, "USD");

        Action act = () => money1.Add(money2);
        
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void SumarMoneyConOperador()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(50, "EUR");
        
        Money money3 = money1 + money2;
        
        Assert.Equal(150, money3.Amount);
        Assert.Equal("EUR", money3.Currency);

        Assert.Equal(100, money1.Amount);
        Assert.Equal(50, money2.Amount);
    }

    [Fact]
    public void SubtractMoneyConOperador()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(50, "EUR");
        Money money3 = money1 - money2;
        Assert.Equal(50, money3.Amount);
        Assert.Equal("EUR", money3.Currency);
        Assert.Equal(100, money1.Amount);
        Assert.Equal(50, money2.Amount);
        
    }

    [Fact]
    public void InvalidOperationExceptionSubtractMoney()
    {
        Money money1 = new Money(50, "EUR");
        Money money2 = new Money(100, "EUR");

        Action act = () => money1.Subtract(money2);
        
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void RestarMoneyResultadoCero()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(100, "EUR");
        
        Money money3 = money1 - money2;
        Assert.Equal(0, money3.Amount);
        Assert.Equal("EUR", money3.Currency);
        Assert.Equal(100, money1.Amount);
        Assert.Equal(100, money2.Amount);
    }
    
    [Fact]
    public void RestarMoneyDiferenteMoneda()
    {
        Money money1 = new Money(100, "EUR");
        Money money2 = new Money(50, "USD");

        Action act = () => money1.Subtract(money2);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MultiplicarArgumentException()
    {
        Money money1 = new Money(100, "EUR");
        decimal factor = -1;
        Action act = () => money1.Multiply(factor);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void MultiplicarArgumentResultadoCero()
    {
        Money money1 = new Money(100, "EUR");
        decimal factor = 0;
        Money result=money1.Multiply(factor);
        Assert.Equal(0, result.Amount);
        Assert.Equal("EUR", result.Currency);
        
    }

    [Fact]
    public void MultiplicarOperator()
    {
        Money money1 = new Money(100, "EUR");
        decimal factor = 2;
        Money result = money1 * factor;
        Assert.Equal(200, result.Amount);
        Assert.Equal("EUR", result.Currency);
    }

    [Fact]
    public void DividirOperator()
    {
        Money money1 = new Money(100, "EUR");
        decimal factor = 2;
        Money result = money1 / factor;
        Assert.Equal(50, result.Amount);
        Assert.Equal("EUR", result.Currency);
    }

    [Fact]
    public void DividirPorUno()
    {
        Money money1 = new Money(100, "EUR");
        decimal factor = 1;
        Money result = money1 / factor;
        Assert.Equal(100, result.Amount);
        Assert.Equal("EUR", result.Currency);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void DividirPorNegativoInvalido(decimal divisor)
    {
        Money money1 = new Money(100, "EUR");
        Action act = () => money1.Divide(divisor);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void PermitirAmountCero()
    {
        var money =new Money(0,"EUR");
        
        Assert.Equal(0,money.Amount);
        Assert.Equal("EUR", money.Currency);
    }
}