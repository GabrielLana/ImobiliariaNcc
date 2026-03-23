using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Vendas.Commands;
using ImobiliariaNcc.Application.Modules.Vendas.Queries;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using NSubstitute;

namespace ImobiliariaNcc.Tests.Handlers;

public class VendaHandlerTests
{
    private readonly IVendasRepository _vendaRepo;
    private readonly IReservasRepository _reservaRepo;
    private readonly IApartamentosRepository _apRepo;
    private readonly IUnitOfWork _uow;

    public VendaHandlerTests()
    {
        _vendaRepo = Substitute.For<IVendasRepository>();
        _reservaRepo = Substitute.For<IReservasRepository>();
        _apRepo = Substitute.For<IApartamentosRepository>();
        _uow = Substitute.For<IUnitOfWork>();
    }

    #region CreateVendaHandler

    [Fact]
    public async Task Create_DeveConcluirVenda_QuandoReservaEApartamentoExistem()
    {
        var handler = new CreateVendaHandler(_vendaRepo, _reservaRepo, _apRepo, _uow);
        var command = new CreateVendaCommand(IdCliente: 1, IdApartamento: 10, IdVendedor: 100);

        var reserva = CriarReservaMock(1, 1, 10);
        var apartamento = CriarApartamentoMock(10);

        _reservaRepo.GetReservaAtiva(1, 10, Arg.Any<CancellationToken>()).Returns(reserva);
        _apRepo.Get(10, Arg.Any<CancellationToken>()).Returns(apartamento);

        await handler.Handle(command, CancellationToken.None);

        apartamento.Ocupado.Should().BeTrue();
        reserva.Ativo.Should().BeFalse();

        _apRepo.Received(1).Update(apartamento);
        _reservaRepo.Received(1).Update(reserva);
        _vendaRepo.Received(1).Create(Arg.Any<VendaModel>());

        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Create_DeveLancarNotFound_QuandoReservaNaoForEncontrada()
    {
        var handler = new CreateVendaHandler(_vendaRepo, _reservaRepo, _apRepo, _uow);
        _reservaRepo.GetReservaAtiva(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
                    .Returns((ReservaModel)null);

        var act = async () => await handler.Handle(new CreateVendaCommand(1, 1, 1), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Reserva não encontrada");
        _vendaRepo.DidNotReceive().Create(Arg.Any<VendaModel>());
    }

    [Fact]
    public async Task Create_DeveLancarBadRequest_QuandoApartamentoJaEstiverOcupado()
    {
        var handler = new CreateVendaHandler(_vendaRepo, _reservaRepo, _apRepo, _uow);
        var reserva = CriarReservaMock(1, 1, 1);
        var apartamento = CriarApartamentoMock(1);
        apartamento.MarcarComoOcupado();

        _reservaRepo.GetReservaAtiva(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(reserva);
        _apRepo.Get(1, Arg.Any<CancellationToken>()).Returns(apartamento);

        var act = async () => await handler.Handle(new CreateVendaCommand(1, 1, 1), CancellationToken.None);

        await act.Should().ThrowAsync<BadRequestException>();
    }

    #endregion

    #region Update e Delete

    [Fact]
    public async Task Update_DeveAtualizarECommitar_QuandoVendaExiste()
    {
        var handler = new UpdateVendaHandler(_vendaRepo, _uow);
        var venda = new VendaModel(1, 1, 1);
        _vendaRepo.Get(1, Arg.Any<CancellationToken>()).Returns(venda);

        var command = new UpdateVendaCommand(1, 2, 2, 2);
        await handler.Handle(command, CancellationToken.None);

        venda.IdCliente.Should().Be(2);
        _vendaRepo.Received(1).Update(venda);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region QueryHandlers (Get e GetAll)

    [Fact]
    public async Task GetAll_DeveRetornarListaDeVendasDtos_QuandoExistiremVendas()
    {
        var handler = new GetAllVendasHandler(_vendaRepo);
        var vendas = new List<VendaModel>
        {
            CriarVendaMock(id: 1, idCliente: 1, idApartamento: 10, idVendedor: 100),
            CriarVendaMock(id: 2, idCliente: 2, idApartamento: 20, idVendedor: 200)
        };

        _vendaRepo.GetAll(Arg.Any<CancellationToken>()).Returns(vendas);

        var result = await handler.Handle(new GetAllVendasQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().Contain(x => x.Id == 1 && x.IdVendedor == 100);
        result.Should().Contain(x => x.Id == 2 && x.IdVendedor == 200);
        await _vendaRepo.Received(1).GetAll(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_DeveRetornarDto_QuandoVendaExiste()
    {
        var handler = new GetVendaHandler(_vendaRepo);
        var venda = CriarVendaMock(id: 1, idCliente: 5, idApartamento: 50, idVendedor: 500);
        _vendaRepo.Get(1, Arg.Any<CancellationToken>()).Returns(venda);

        var result = await handler.Handle(new GetVendaQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.IdCliente.Should().Be(5);
        result.IdApartamento.Should().Be(50);
        result.IdVendedor.Should().Be(500);
    }

    [Fact]
    public async Task GetById_DeveRetornarNull_QuandoVendaNaoExiste()
    {
        var handler = new GetVendaHandler(_vendaRepo);
        _vendaRepo.Get(99, Arg.Any<CancellationToken>()).Returns((VendaModel)null);

        var result = await handler.Handle(new GetVendaQuery(99), CancellationToken.None);

        result.Should().BeNull();
    }

    #endregion

    #region Helper Adicional

    private VendaModel CriarVendaMock(int id, int idCliente, int idApartamento, int idVendedor)
    {
        var venda = new VendaModel(idCliente, idApartamento, idVendedor);
        venda.GetType().GetProperty("Id")?.SetValue(venda, id);
        return venda;
    }

    #endregion

    #region Helpers
    private ApartamentoModel CriarApartamentoMock(int id)
    {
        var ap = new ApartamentoModel(50, 1, 1, 1, "D", "C", 1, 1, 100, 10, 5, "0", "R", "B", "1", "S", "C", null);
        ap.GetType().GetProperty("Id")?.SetValue(ap, id);
        return ap;
    }

    private ReservaModel CriarReservaMock(int id, int idCliente, int idApartamento)
    {
        var res = new ReservaModel(idCliente, idApartamento);
        res.GetType().GetProperty("Id")?.SetValue(res, id);
        return res;
    }
    #endregion
}