using FluentAssertions;
using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Tests.Models;

public class BaseModelTests
{
    private class TestModel : BaseModel { }

    [Fact]
    public void AtualizarDataAlteracao_DeveDefinirDataAtual()
    {
        var model = new TestModel();

        model.AtualizarDataAlteracao();

        model.DataAlteracao.Should().NotBeNull();
        model.DataAlteracao.Value.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}
