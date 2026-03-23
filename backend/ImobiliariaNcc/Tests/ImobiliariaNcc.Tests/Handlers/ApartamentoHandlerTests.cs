using Bogus;
using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Apartamentos.Commands;
using ImobiliariaNcc.Application.Modules.Apartamentos.Queries;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using NSubstitute;
using Xunit;

namespace ImobiliariaNcc.Tests.Handlers;

public class ApartamentoHandlerTests
{
    private readonly IApartamentosRepository _repository;
    private readonly IReservasRepository _reservaRepository;
    private readonly IUnitOfWork _uow;
    private readonly Faker _faker;

    public ApartamentoHandlerTests()
    {
        _repository = Substitute.For<IApartamentosRepository>();
        _reservaRepository = Substitute.For<IReservasRepository>();
        _uow = Substitute.For<IUnitOfWork>();
        _faker = new Faker("pt_BR");
    }

    #region CreateApartamentoHandler

    [Fact]
    public async Task Create_DevePersistirComSucesso_QuandoDadosSaoValidos()
    {
        var handler = new CreateApartamentoHandler(_repository, _uow);
        var command = CriarCreateCommand();

        await handler.Handle(command, CancellationToken.None);

        _repository.Received(1).Create(Arg.Any<ApartamentoModel>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region UpdateApartamentoHandler

    [Fact]
    public async Task Update_DeveAtualizarECommitar_QuandoApartamentoExiste()
    {
        var handler = new UpdateApartamentoHandler(_repository, _uow);
        var command = CriarUpdateCommand();
        var apartamentoExistente = CriarApartamentoMock();

        _repository.Get(command.Id, Arg.Any<CancellationToken>()).Returns(apartamentoExistente);

        await handler.Handle(command, CancellationToken.None);

        _repository.Received(1).Update(Arg.Is<ApartamentoModel>(x => x.Ocupado == command.Ocupado));
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Update_DeveLancarNotFoundException_QuandoApartamentoNaoExistir()
    {
        var handler = new UpdateApartamentoHandler(_repository, _uow);
        var command = CriarUpdateCommand();
        _repository.Get(command.Id, Arg.Any<CancellationToken>()).Returns((ApartamentoModel)null);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Apartamento não encontrado");

        _repository.DidNotReceive().Update(Arg.Any<ApartamentoModel>());
        await _uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region DeleteApartamentoHandler

    [Fact]
    public async Task Delete_DeveRemoverECommitar_QuandoApartamentoExiste()
    {
        var handler = new DeleteApartamentoHandler(_repository, _uow);
        var command = new DeleteApartamentoCommand(1);
        var apartamento = CriarApartamentoMock();

        _repository.Get(command.Id, Arg.Any<CancellationToken>()).Returns(apartamento);

        await handler.Handle(command, CancellationToken.None);

        _repository.Received(1).Delete(apartamento);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Delete_DeveLancarNotFoundException_QuandoIdNaoExistir()
    {
        var handler = new DeleteApartamentoHandler(_repository, _uow);
        var command = new DeleteApartamentoCommand(999);
        _repository.Get(999, Arg.Any<CancellationToken>()).Returns((ApartamentoModel)null);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Apartamento não encontrado");

        _repository.DidNotReceive().Delete(Arg.Any<ApartamentoModel>());
        await _uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region GetAllApartamentosHandler

    [Fact]
    public async Task GetAll_DeveRetornarListaDeDtos_QuandoExistiremDados()
    {
        var handler = new GetAllApartamentosHandler(_repository);
        var listaModels = new List<ApartamentoModel> { CriarApartamentoMock(), CriarApartamentoMock() };
        _repository.GetAll(Arg.Any<CancellationToken>()).Returns(listaModels);

        var result = await handler.Handle(new GetAllApartamentosQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.First().Bairro.Should().Be(listaModels.First().Bairro);
        await _repository.Received(1).GetAll(Arg.Any<CancellationToken>());
    }

    #endregion

    #region GetApartamentoHandler

    [Fact]
    public async Task GetById_DeveRetornarDto_QuandoApartamentoExiste()
    {
        var handler = new GetApartamentoHandler(_repository);
        var apartamento = CriarApartamentoMock();
        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(apartamento);

        var result = await handler.Handle(new GetApartamentoQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(apartamento.Id);
        result.Logradouro.Should().Be(apartamento.Logradouro);
    }

    [Fact]
    public async Task GetById_DeveRetornarNull_QuandoApartamentoNaoExiste()
    {
        var handler = new GetApartamentoHandler(_repository);
        _repository.Get(99, Arg.Any<CancellationToken>()).Returns((ApartamentoModel)null);

        var result = await handler.Handle(new GetApartamentoQuery(99), CancellationToken.None);

        result.Should().BeNull();
    }

    #endregion

    #region ListApartamentosDisponiveisHandler

    [Fact]
    public async Task ListDisponiveis_DeveFiltrarApenasNaoOcupados_EMarcarReservados()
    {
        var handler = new ListApartamentosDisponiveisHandler(_repository, _reservaRepository);

        var apOcupado = CriarApartamentoMock();
        SetId(apOcupado, 1);
        apOcupado.MarcarComoOcupado();

        var apLivre = CriarApartamentoMock();
        SetId(apLivre, 2);

        var apReservado = CriarApartamentoMock();
        SetId(apReservado, 3);

        var listaAps = new List<ApartamentoModel> { apOcupado, apLivre, apReservado };

        var listaReservas = new List<ReservaModel> {
        new ReservaModel(1, 3)
    };

        _repository.GetAll(Arg.Any<CancellationToken>()).Returns(listaAps);
        _reservaRepository.GetAll(Arg.Any<CancellationToken>()).Returns(listaReservas);

        var result = await handler.Handle(new ListApartamentosDisponiveisQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().ContainSingle(x => x.Id == 3 && x.Reservado);
        result.Should().ContainSingle(x => x.Id == 2 && !x.Reservado);
    }

    private void SetId(BaseModel model, int id)
    {
        model.GetType()
             .GetProperty("Id")?
             .SetValue(model, id, null);
    }

    #endregion

    #region Helpers

    private CreateApartamentoCommand CriarCreateCommand() =>
        new(100, 3, 2, 1, "Luxo", "Completo", 10, 2, 800000, 1000, 200, "01234567", "Av Paulista", "Bela Vista", "1000", "SP", "São Paulo", null);

    private UpdateApartamentoCommand CriarUpdateCommand() =>
        new(1, true, 100, 3, 2, 1, "Luxo", "Completo", 10, 2, 800000, 1000, 200, "01234567", "Av Paulista", "Bela Vista", "1000", "SP", "São Paulo", null);

    private ApartamentoModel CriarApartamentoMock() =>
        new(50, 1, 1, 0, "Simples", "Básico", 1, 1, 200000, 300, 50, "00000000", "Rua X", "Bairro Y", "1", "Estado", "Cidade", null);

    #endregion
}