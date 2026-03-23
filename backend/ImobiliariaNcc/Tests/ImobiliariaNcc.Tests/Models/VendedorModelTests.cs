using FluentAssertions;
using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Tests.Models;

public class VendedorModelTests
{
    [Fact]
    public void AtualizaSenha_DeveAlterarPropriedadeSenha()
    {
        var vendedor = new VendedorModel("Nome", "123", "SenhaAntiga", DateTime.Now, "e@e.com", "11", "00", "Rua", "B", "1", null, "S", 1);

        vendedor.AtualizaSenha("NovaSenha123");

        vendedor.Senha.Should().Be("NovaSenha123");
    }
}
