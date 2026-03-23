using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Clientes.Commands;
using ImobiliariaNcc.Application.Modules.Clientes.Queries;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using NSubstitute;

namespace ImobiliariaNcc.Tests.Handlers;

public class ClienteHandlerTests
{
    private readonly IClientesRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly Faker _faker;

    public ClienteHandlerTests()
    {
        _repository = Substitute.For<IClientesRepository>();
        _uow = Substitute.For<IUnitOfWork>();
        _faker = new Faker("pt_BR");
    }

    #region CreateClienteHandler
    [Fact]
    public async Task Create_DevePersistirClienteERetornarId_QuandoDadosValidos()
    {
        var handler = new CreateClienteHandler(_repository, _uow);
        var command = new CreateClienteCommand(
            _faker.Person.FullName, _faker.Person.Cpf(), DateTime.Now.AddYears(-30),
            _faker.Internet.Email(), "11999998888", "Casado");

        var result = await handler.Handle(command, CancellationToken.None);

        _repository.Received(1).Create(Arg.Any<ClienteModel>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }
    #endregion

    #region UpdateClienteHandler
    [Fact]
    public async Task Update_DeveAtualizarECommitar_QuandoClienteExiste()
    {
        var handler = new UpdateClienteHandler(_repository, _uow);
        var clienteExistente = CriarClienteMock(1);
        var command = new UpdateClienteCommand(1, "Novo Nome", clienteExistente.Cpf,
            clienteExistente.DataNascimento, "novo@email.com", "11999999999", "Solteiro", true);

        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(clienteExistente);

        await handler.Handle(command, CancellationToken.None);

        clienteExistente.Nome.Should().Be("Novo Nome");
        clienteExistente.Email.Should().Be("novo@email.com");
        _repository.Received(1).Update(clienteExistente);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Update_DeveLancarNotFoundException_QuandoClienteNaoExiste()
    {
        var handler = new UpdateClienteHandler(_repository, _uow);
        var command = new UpdateClienteCommand(99, "Nome", "000", DateTime.Now, "e@e.com", "11", "S", true);
        _repository.Get(99, Arg.Any<CancellationToken>()).Returns((ClienteModel)null);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Cliente não encontrado");
        _repository.DidNotReceive().Update(Arg.Any<ClienteModel>());
        await _uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
    #endregion

    #region DeleteClienteHandler
    [Fact]
    public async Task Delete_DeveRemoverECommitar_QuandoClienteExiste()
    {
        var handler = new DeleteClienteHandler(_repository, _uow);
        var cliente = CriarClienteMock(1);
        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(cliente);

        await handler.Handle(new DeleteClienteCommand(1), CancellationToken.None);

        _repository.Received(1).Delete(cliente);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Delete_DeveLancarNotFoundException_QuandoIdInexistente()
    {
        var handler = new DeleteClienteHandler(_repository, _uow);
        _repository.Get(99, Arg.Any<CancellationToken>()).Returns((ClienteModel)null);

        var act = async () => await handler.Handle(new DeleteClienteCommand(99), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }
    #endregion

    #region QueryHandlers (Get e GetAll)
    [Fact]
    public async Task GetAll_DeveRetornarListaDeDtos_MapeadaCorretamente()
    {
        var handler = new GetAllClientesHandler(_repository);
        var clientes = new List<ClienteModel> { CriarClienteMock(1), CriarClienteMock(2) };
        _repository.GetAll(Arg.Any<CancellationToken>()).Returns(clientes);

        var result = await handler.Handle(new GetAllClientesQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.First().Nome.Should().Be(clientes.First().Nome);
    }

    [Fact]
    public async Task GetById_DeveRetornarDto_QuandoClienteExiste()
    {
        var handler = new GetClienteHandler(_repository);
        var cliente = CriarClienteMock(1);
        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(cliente);

        var result = await handler.Handle(new GetClienteQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Nome.Should().Be(cliente.Nome);
        result.Id.Should().Be(1);
    }
    #endregion

    #region Helpers
    private ClienteModel CriarClienteMock(int id)
    {
        var cliente = new ClienteModel(
            _faker.Person.FullName, _faker.Person.Cpf(false), DateTime.Now.AddYears(-20),
            _faker.Internet.Email(), "11999998888", "Solteiro");

        SetId(cliente, id);
        return cliente;
    }

    private void SetId(BaseModel model, int id)
    {
        model.GetType().GetProperty("Id")?.SetValue(model, id, null);
    }
    #endregion
}