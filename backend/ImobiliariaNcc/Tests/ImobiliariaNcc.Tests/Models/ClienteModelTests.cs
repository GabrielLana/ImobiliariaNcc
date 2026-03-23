using FluentAssertions;
using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Tests.Models;

public class ClienteModelTests
{
    [Fact]
    public void Desativar_DeveAlterarAtivoParaFalse()
    {
        var cliente = new ClienteModel("Nome", "12345678901", DateTime.Now.AddYears(-20), "email@test.com", "1199999999", "Solteiro");

        cliente.Desativar();

        cliente.Ativo.Should().BeFalse();
    }
}
