using FluentAssertions;
using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Tests.Models;

public class ReservaModelTests
{
    [Fact]
    public void Construtor_DeveDefinirAtivoComoTrue()
    {
        var reserva = new ReservaModel(1, 1);
        reserva.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Desativar_DeveMudarEstado()
    {
        var reserva = new ReservaModel(1, 1);
        reserva.Desativar();
        reserva.Ativo.Should().BeFalse();
    }
}