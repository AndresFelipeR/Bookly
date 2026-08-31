using Bookly.Application.Common.Exceptions;
using Bookly.Application.Common.Interfaces.Persistence;
using Bookly.Application.Servicios.Commands.Create;
using Bookly.Domain.Entities;
using Moq;

namespace Bookly.UnitTest.Application.Servicios.Commands.Create;

public class CreateServicioCommandHandlerTests
{
    [Fact]
    public async Task Handle_CuandoTipoServicioNoExiste_LanzaNotFoundException()
    {
        //Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var tipoServicioRepositoryMock = new Mock<ITipoServicioRepository>();
        var servicioRepositoryMock = new Mock<IServicioRepository>();
        
        var tipoServicioId = Guid.NewGuid();

        var command = new CreateServicioCommand(
            "Consulta Tarot",
            "Consulta de tarot",
            50,
            "EUR",
            tipoServicioId,
            60,
            30,
            60);
        
        var handler = new CreateServicioCommandHandler(
            unitOfWorkMock.Object,
            tipoServicioRepositoryMock.Object,
            servicioRepositoryMock.Object);
        
        tipoServicioRepositoryMock
            .Setup(x => x.GetByIdAsync(
                tipoServicioId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((TipoServicio?)null);
        
        //Act y Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => handler.Handle(command, CancellationToken.None));
        
        servicioRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<Servicio>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_CuandoTipoServicioExiste_CreaServicioCorrectamente()
    {
        //Arrenge
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var tipoServicioRepositoryMock = new Mock<ITipoServicioRepository>();
        var servicioRepositoryMock = new Mock<IServicioRepository>();
        
        

        var tipoServicio = TipoServicio.Create(
            "Consulta Tarot",
            "Consulta de tarot");

        var tipoServicioId = tipoServicio.Id;
        var command = new CreateServicioCommand(
            "Consulta de tarot",
            "Consulta de tarot",
            50,
            "EUR",
            tipoServicioId,
            60,
            30,
            60);

        var handler = new CreateServicioCommandHandler(
            unitOfWorkMock.Object,
            tipoServicioRepositoryMock.Object,
            servicioRepositoryMock.Object);
        
        tipoServicioRepositoryMock
            .Setup(x => x.GetByIdAsync(
                tipoServicioId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(tipoServicio);
        
        //Act
         var result = await handler.Handle(command, CancellationToken.None);
         
         //Assert
         Assert.NotNull(result);

         Assert.Equal(command.Nombre, result.Nombre);
         Assert.Equal(command.Descripcion, result.Descripcion);
         Assert.Equal(command.Amount, result.Amount);
         Assert.Equal(command.Currency, result.Currency);
         Assert.Equal(command.TipoServicioId, result.TipoServicioId);
         Assert.Equal(command.Duracion, result.Duracion);
         Assert.Equal(command.MargenCancelacion, result.MargenCancelacion);
         Assert.Equal(command.MargenAnticipacion, result.MargenAnticipacion);
         
         servicioRepositoryMock.Verify(
             x => x.AddAsync(
                 It.Is<Servicio>(s =>
                     s.Nombre == command.Nombre &&
                     s.Precio.Amount == command.Amount &&
                     s.Precio.Currency == command.Currency &&
                     s.Duracion.TotalMinutes == command.Duracion &&
                     s.TipoServicio == tipoServicio),
                 It.IsAny<CancellationToken>()),
             Times.Once);
         
         unitOfWorkMock.Verify(
             x => x.SaveChangesAsync(
                 It.IsAny<CancellationToken>()),
             Times.Once);
    }

    [Fact]
    public async Task Handle_CuandoTipoServicioExiste_CrearServicioCallback()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var tipoServicioRepositoryMock = new Mock<ITipoServicioRepository>();
        var servicioRepositoryMock = new Mock<IServicioRepository>();
        
        Servicio? servicioCreado = null;
        
        var tipoServicio = TipoServicio.Create(
            "Consulta Tarot",
            "Consulta de tarot");

        var tipoServicioId = tipoServicio.Id;
        var command = new CreateServicioCommand(
            "Consulta de tarot",
            "Consulta de tarot",
            50,
            "EUR",
            tipoServicioId,
            60,
            30,
            60);

        var handler = new CreateServicioCommandHandler(
            unitOfWorkMock.Object,
            tipoServicioRepositoryMock.Object,
            servicioRepositoryMock.Object);
        
        tipoServicioRepositoryMock
            .Setup(x => x.GetByIdAsync(
                tipoServicioId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(tipoServicio);

        servicioRepositoryMock.
            Setup(x => x.AddAsync(
                It.IsAny<Servicio>(),
                It.IsAny<CancellationToken>()))
            .Callback<Servicio, CancellationToken>((s, c) =>
            {
                servicioCreado = s;
            });
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
       Assert.NotNull(servicioCreado);
       Assert.Equal(command.Nombre, servicioCreado.Nombre);
       Assert.Equal(command.Descripcion, servicioCreado.Descripcion);
       Assert.Equal(command.Amount,servicioCreado.Precio.Amount);
     
    }
}