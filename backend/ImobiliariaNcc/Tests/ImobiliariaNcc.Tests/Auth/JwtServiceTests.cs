using FluentAssertions;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ImobiliariaNcc.Tests.Auth;

public class JwtServiceTests
{
    private readonly IConfiguration _config;
    private readonly JwtService _jwtService;
    private const string DummyKey = "minha_chave_super_secreta_com_mais_de_32_caracteres";

    public JwtServiceTests()
    {
        _config = Substitute.For<IConfiguration>();

        _config["Jwt:Key"].Returns(DummyKey);
        _config["Jwt:Issuer"].Returns("ImobiliariaNcc");
        _config["Jwt:Audience"].Returns("ImobiliariaNccUsers");

        _jwtService = new JwtService(_config);
    }

    [Fact]
    public void GenerateToken_DeveRetornarTokenValido_ComClaimsCorretas()
    {
        var vendedor = new VendedorModel(
            "Fulano Vendedor", "12345678901", "hash", DateTime.Now.AddYears(-30),
            "vendedor@teste.com", "11999998888", "01234567", "Rua A", "Bairro", "1", null, "Vendas", 1001
        );
        vendedor.GetType().GetProperty("Id")?.SetValue(vendedor, 123);

        var tokenString = _jwtService.GenerateToken(vendedor);

        tokenString.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(tokenString);

        jsonToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "123");
        jsonToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "Fulano Vendedor");

        jsonToken.Issuer.Should().Be("ImobiliariaNcc");
        jsonToken.Audiences.Should().Contain("ImobiliariaNccUsers");

        jsonToken.ValidTo.Should().BeAfter(DateTime.UtcNow.AddHours(7));
    }

    [Fact]
    public void GenerateToken_DeveUsarAlgoritmoHmacSha256()
    {
        var vendedor = new VendedorModel("Nome", "000", "", DateTime.Now, "e@e.com", "0", "0", "R", "B", "1", null, "S", 1);

        var tokenString = _jwtService.GenerateToken(vendedor);
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(tokenString);

        jsonToken.SignatureAlgorithm.Should().Be("HS256");
    }
}