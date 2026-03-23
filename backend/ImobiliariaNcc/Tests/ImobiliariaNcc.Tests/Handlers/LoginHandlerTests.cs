using FluentAssertions;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Application.Modules.Auth.Commands;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace ImobiliariaNcc.Tests.Handlers;

public class LoginHandlerTests
{
    private readonly IVendedoresRepository _repo;
    private readonly IJwtService _jwt;
    private readonly IPasswordHasher<VendedorModel> _passwordHasher;
    private readonly LoginHandler _handler;

    public LoginHandlerTests()
    {
        _repo = Substitute.For<IVendedoresRepository>();
        _jwt = Substitute.For<IJwtService>();
        _passwordHasher = Substitute.For<IPasswordHasher<VendedorModel>>();

        _handler = new LoginHandler(_repo, _jwt, _passwordHasher);
    }

    [Fact]
    public async Task Handle_QuandoCredenciaisSaoValidas_DeveRetornarTokenENome()
    {
        var command = new LoginCommand("12345678901", "Senha123!");
        var vendedor = CriarVendedorMock(command.Cpf);

        _repo.GetByCpf(command.Cpf, Arg.Any<CancellationToken>()).Returns(vendedor);

        _passwordHasher.VerifyHashedPassword(vendedor, vendedor.Senha, command.Senha)
                       .Returns(PasswordVerificationResult.Success);

        _jwt.GenerateToken(vendedor).Returns("token-jwt-gerado");

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Token.Should().Be("token-jwt-gerado");
        result.Nome.Should().Be(vendedor.Nome);
    }

    [Fact]
    public async Task Handle_QuandoVendedorNaoExiste_DeveLancarUnauthorizedException()
    {
        var command = new LoginCommand("00000000000", "senha");
        _repo.GetByCpf(command.Cpf, Arg.Any<CancellationToken>()).Returns((VendedorModel)null);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedException>()
                 .WithMessage("Credenciais inválidas");

        _jwt.DidNotReceive().GenerateToken(Arg.Any<VendedorModel>());
    }

    [Fact]
    public async Task Handle_QuandoSenhaIncorreta_DeveLancarUnauthorizedException()
    {
        var command = new LoginCommand("12345678901", "SenhaErrada");
        var vendedor = CriarVendedorMock(command.Cpf);

        _repo.GetByCpf(command.Cpf, Arg.Any<CancellationToken>()).Returns(vendedor);

        _passwordHasher.VerifyHashedPassword(vendedor, vendedor.Senha, command.Senha)
                       .Returns(PasswordVerificationResult.Failed);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedException>()
                 .WithMessage("Credenciais inválidas");

        _jwt.DidNotReceive().GenerateToken(Arg.Any<VendedorModel>());
    }

    private VendedorModel CriarVendedorMock(string cpf)
    {
        return new VendedorModel(
            "Vendedor Teste",
            cpf,
            "hash-da-senha",
            DateTime.Now.AddYears(-25),
            "vendedor@teste.com",
            "11999998888",
            "01234567",
            "Rua Teste",
            "Bairro",
            "123",
            null,
            "Vendas",
            1001
        );
    }
}