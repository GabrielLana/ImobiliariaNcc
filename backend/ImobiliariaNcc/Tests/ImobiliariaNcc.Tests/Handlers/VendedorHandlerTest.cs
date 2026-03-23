using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Vendedores.Commands;
using ImobiliariaNcc.Application.Modules.Vendedores.Queries;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace ImobiliariaNcc.Tests.Handlers;

public class VendedorHandlerTests
{
    private readonly IVendedoresRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher<VendedorModel> _passwordHasher;
    private readonly Faker _faker;

    public VendedorHandlerTests()
    {
        _repository = Substitute.For<IVendedoresRepository>();
        _uow = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher<VendedorModel>>();
        _faker = new Faker("pt_BR");
    }

    #region CreateVendedorHandler

    [Fact]
    public async Task Create_DeveGerarHashDaSenhaEPersistir_QuandoDadosValidos()
    {
        var handler = new CreateVendedorHandler(_repository, _uow, _passwordHasher);
        var command = CriarCreateCommand();

        _passwordHasher.HashPassword(Arg.Any<VendedorModel>(), command.Senha)
                       .Returns("senha_hash_segura");

        await handler.Handle(command, CancellationToken.None);

        _passwordHasher.Received(1).HashPassword(Arg.Any<VendedorModel>(), command.Senha);

        _repository.Received(1).Create(Arg.Is<VendedorModel>(v =>
            v.Nome == command.Nome &&
            v.Senha == "senha_hash_segura"
        ));

        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region UpdateVendedorHandler

    [Fact]
    public async Task Update_DeveAtualizarDados_QuandoVendedorExiste()
    {
        var handler = new UpdateVendedorHandler(_repository, _uow);
        var vendedorExistente = CriarVendedorMock(1);
        var command = CriarUpdateCommand(1);

        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(vendedorExistente);

        await handler.Handle(command, CancellationToken.None);

        vendedorExistente.Nome.Should().Be(command.Nome);
        vendedorExistente.Setor.Should().Be(command.Setor);

        _repository.Received(1).Update(vendedorExistente);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Update_DeveLancarNotFound_QuandoVendedorNaoExiste()
    {
        var handler = new UpdateVendedorHandler(_repository, _uow);
        _repository.Get(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns((VendedorModel)null);

        var act = async () => await handler.Handle(CriarUpdateCommand(99), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
        await _uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region DeleteVendedorHandler

    [Fact]
    public async Task Delete_DeveRemover_QuandoVendedorExiste()
    {
        var handler = new DeleteVendedorHandler(_repository, _uow);
        var vendedor = CriarVendedorMock(1);
        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(vendedor);

        await handler.Handle(new DeleteVendedorCommand(1), CancellationToken.None);

        _repository.Received(1).Delete(vendedor);
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region QueryHandlers (Get e GetAll)

    [Fact]
    public async Task GetAll_DeveRetornarListaDeVendedores_QuandoExistiremDados()
    {
        var handler = new GetAllVendedoresHandler(_repository);
        var vendedores = new List<VendedorModel>
        {
            CriarVendedorMock(1),
            CriarVendedorMock(2)
        };

        _repository.GetAll(Arg.Any<CancellationToken>()).Returns(vendedores);

        var result = await handler.Handle(new GetAllVendedoresQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.First().Nome.Should().Be(vendedores.First().Nome);
        result.First().NumeroRegistro.Should().Be(vendedores.First().NumeroRegistro);
        await _repository.Received(1).GetAll(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_DeveRetornarVendedorDto_QuandoVendedorExiste()
    {
        var handler = new GetVendedorHandler(_repository);
        var vendedor = CriarVendedorMock(1);
        _repository.Get(1, Arg.Any<CancellationToken>()).Returns(vendedor);

        var result = await handler.Handle(new GetVendedorQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Email.Should().Be(vendedor.Email);
        result.Setor.Should().Be(vendedor.Setor);
    }

    [Fact]
    public async Task GetById_DeveRetornarNull_QuandoVendedorNaoExiste()
    {
        var handler = new GetVendedorHandler(_repository);
        _repository.Get(999, Arg.Any<CancellationToken>()).Returns((VendedorModel)null);

        var result = await handler.Handle(new GetVendedorQuery(999), CancellationToken.None);

        result.Should().BeNull();
    }

    #endregion

    #region Helpers

    private CreateVendedorCommand CriarCreateCommand() =>
        new(_faker.Person.FullName, _faker.Person.Cpf(), "Senha@123", DateTime.Now.AddYears(-25),
            _faker.Internet.Email(), "11999998888", "Vendas", 1001, "01234567", "Rua A", "Bairro B", "10", null);

    private UpdateVendedorCommand CriarUpdateCommand(int id) =>
        new(id, true, "Nome Atualizado", _faker.Person.Cpf(), DateTime.Now.AddYears(-25),
            "email@novo.com", "11999998888", "Gerência", 2002, "01234567", "Rua A", "Bairro B", "10", null);

    private VendedorModel CriarVendedorMock(int id)
    {
        var vendedor = new VendedorModel("Vendedor", "000", "hash", DateTime.Now, "e@e.com", "00", "0", "R", "B", "1", null, "S", 1);
        vendedor.GetType().GetProperty("Id")?.SetValue(vendedor, id);
        return vendedor;
    }

    #endregion
}