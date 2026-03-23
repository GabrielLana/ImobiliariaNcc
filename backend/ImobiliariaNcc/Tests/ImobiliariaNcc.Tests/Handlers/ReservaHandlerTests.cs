using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Reservas.Commands;
using ImobiliariaNcc.Application.Modules.Reservas.Queries;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using NSubstitute;

namespace ImobiliariaNcc.Tests.Handlers;

public class ReservaHandlerTests
{
    private readonly IReservasRepository _reservaRepo;
    private readonly IApartamentosRepository _apRepo;
    private readonly IUnitOfWork _uow;

    public ReservaHandlerTests()
    {
        _reservaRepo = Substitute.For<IReservasRepository>();
        _apRepo = Substitute.For<IApartamentosRepository>();
        _uow = Substitute.For<IUnitOfWork>();
    }

    #region CreateReservaHandler

    [Fact]
    public async Task Create_DeveCriarReserva_QuandoTudoEstiverValido()
    {
        var handler = new CreateReservaHandler(_reservaRepo, _apRepo, _uow);
        var command = new CreateReservaCommand(IdCliente: 1, IdApartamento: 1);

        var ap = CriarApartamentoMock(1);
        _apRepo.Get(1, Arg.Any<CancellationToken>()).Returns(ap);

        _reservaRepo.ClientePossuiReservaAtiva(1, Arg.Any<CancellationToken>()).Returns(false);
        _reservaRepo.ApartamentoPossuiReservaAtiva(1, Arg.Any<CancellationToken>()).Returns(false);

        await handler.Handle(command, CancellationToken.None);

        _reservaRepo.Received(1).Create(Arg.Any<ReservaModel>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Create_DeveLancarNotFound_QuandoApartamentoNaoExistir()
    {
        var handler = new CreateReservaHandler(_reservaRepo, _apRepo, _uow);
        _apRepo.Get(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns((ApartamentoModel)null);

        var act = async () => await handler.Handle(new CreateReservaCommand(1, 99), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Apartamento não encontrado");
    }

    [Fact]
    public async Task Create_DeveLancarBadRequest_QuandoApartamentoJaOcupado()
    {
        var handler = new CreateReservaHandler(_reservaRepo, _apRepo, _uow);
        var ap = CriarApartamentoMock(1);
        ap.MarcarComoOcupado();

        _apRepo.Get(1, Arg.Any<CancellationToken>()).Returns(ap);

        var act = async () => await handler.Handle(new CreateReservaCommand(1, 1), CancellationToken.None);

        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Apartamento já ocupado");
    }

    [Fact]
    public async Task Create_DeveLancarBadRequest_QuandoClienteJaTiverReservaAtiva()
    {
        var handler = new CreateReservaHandler(_reservaRepo, _apRepo, _uow);
        _apRepo.Get(1, Arg.Any<CancellationToken>()).Returns(CriarApartamentoMock(1));

        _reservaRepo.ClientePossuiReservaAtiva(1, Arg.Any<CancellationToken>()).Returns(true);

        var act = async () => await handler.Handle(new CreateReservaCommand(1, 1), CancellationToken.None);

        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Cliente já possui reserva ativa");
    }

    [Fact]
    public async Task Create_DeveLancarBadRequest_QuandoApartamentoJaTiverReservaAtiva()
    {
        var handler = new CreateReservaHandler(_reservaRepo, _apRepo, _uow);
        _apRepo.Get(1, Arg.Any<CancellationToken>()).Returns(CriarApartamentoMock(1));
        _reservaRepo.ClientePossuiReservaAtiva(1, Arg.Any<CancellationToken>()).Returns(false);

        _reservaRepo.ApartamentoPossuiReservaAtiva(1, Arg.Any<CancellationToken>()).Returns(true);

        var act = async () => await handler.Handle(new CreateReservaCommand(1, 1), CancellationToken.None);

        await act.Should().ThrowAsync<BadRequestException>();
    }

    #endregion

    #region Update e Delete

    [Fact]
    public async Task Update_DeveLancarNotFound_QuandoReservaNaoExistir()
    {
        var handler = new UpdateReservaHandler(_reservaRepo, _uow);
        _reservaRepo.Get(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns((ReservaModel)null);

        var act = async () => await handler.Handle(new UpdateReservaCommand(99, 1, 1, true), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Delete_DeveRemoverERetornar_QuandoReservaExiste()
    {
        var handler = new DeleteReservaHandler(_reservaRepo, _uow);
        var reserva = new ReservaModel(1, 1);
        _reservaRepo.Get(1, Arg.Any<CancellationToken>()).Returns(reserva);

        await handler.Handle(new DeleteReservaCommand(1), CancellationToken.None);

        _reservaRepo.Received(1).Delete(reserva);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region QueryHandlers (Get e GetAll)

    [Fact]
    public async Task GetAll_DeveRetornarListaDeDtos_QuandoExistiremReservas()
    {
        var handler = new GetAllReservasHandler(_reservaRepo);
        var reservas = new List<ReservaModel>
        {
            CriarReservaMock(id: 1, idCliente: 10, idApartamento: 100),
            CriarReservaMock(id: 2, idCliente: 20, idApartamento: 200)
        };

        _reservaRepo.GetAll(Arg.Any<CancellationToken>()).Returns(reservas);

        var result = await handler.Handle(new GetAllReservasQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Id == 1 && x.IdCliente == 10 && x.IdApartamento == 100);
        result.Should().Contain(x => x.Id == 2 && x.IdCliente == 20 && x.IdApartamento == 200);
        await _reservaRepo.Received(1).GetAll(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_DeveRetornarDto_QuandoReservaExiste()
    {
        var handler = new GetReservaHandler(_reservaRepo);
        var reserva = CriarReservaMock(id: 1, idCliente: 50, idApartamento: 500);
        _reservaRepo.Get(1, Arg.Any<CancellationToken>()).Returns(reserva);

        var result = await handler.Handle(new GetReservaQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.IdCliente.Should().Be(50);
        result.IdApartamento.Should().Be(500);
        result.Ativo.Should().BeTrue();
    }

    [Fact]
    public async Task GetById_DeveRetornarNull_QuandoReservaNaoExiste()
    {
        var handler = new GetReservaHandler(_reservaRepo);
        _reservaRepo.Get(99, Arg.Any<CancellationToken>()).Returns((ReservaModel)null);

        var result = await handler.Handle(new GetReservaQuery(99), CancellationToken.None);

        result.Should().BeNull();
    }

    #endregion

    #region Helpers Adicionais

    private ReservaModel CriarReservaMock(int id, int idCliente, int idApartamento)
    {
        var reserva = new ReservaModel(idCliente, idApartamento);
        reserva.GetType().GetProperty("Id")?.SetValue(reserva, id);
        return reserva;
    }

    #endregion

    #region Helpers
    private ApartamentoModel CriarApartamentoMock(int id)
    {
        var ap = new ApartamentoModel(50, 2, 1, 1, "D", "C", 1, 1, 100, 10, 5, "00", "R", "B", "1", "S", "C", null);
        ap.GetType().GetProperty("Id")?.SetValue(ap, id);
        return ap;
    }
    #endregion
}