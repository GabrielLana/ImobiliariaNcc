using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace ImobiliariaNcc.Tests.Models;

public class ApartamentoModelTests
{
    [Fact]
    public void Construtor_DeveInicializarCorretamente_EDefinirComoDisponivel()
    {
        // Act
        var apartamento = new ApartamentoModel(100, 3, 2, 1, "Detalhes", "Condo", 5, 1, 500000, 500, 100, "12345678", "Rua", "Bairro", "10", "SP", "Cidade", null);

        // Assert
        apartamento.Ocupado.Should().BeFalse();
        apartamento.EstaDisponivel().Should().BeTrue();
        apartamento.Metragem.Should().Be(100);
    }

    [Fact]
    public void MarcarComoOcupado_QuandoDisponivel_DeveAlterarEstado()
    {
        var apartamento = new ApartamentoModel(100, 3, 2, 1, "Detalhes", "Condo", 5, 1, 500000, 500, 100, "12345678", "Rua", "Bairro", "10", "SP", "Cidade", null);

        apartamento.MarcarComoOcupado();

        apartamento.Ocupado.Should().BeTrue();
        apartamento.EstaDisponivel().Should().BeFalse();
    }

    [Fact]
    public void MarcarComoOcupado_QuandoJaOcupado_DeveLancarBadRequestException()
    {
        var apartamento = new ApartamentoModel(100, 3, 2, 1, "Detalhes", "Condo", 5, 1, 500000, 500, 100, "12345678", "Rua", "Bairro", "10", "SP", "Cidade", null);
        apartamento.MarcarComoOcupado();

        var act = () => apartamento.MarcarComoOcupado();

        act.Should().Throw<BadRequestException>().WithMessage("Apartamento já está ocupado.");
    }
}