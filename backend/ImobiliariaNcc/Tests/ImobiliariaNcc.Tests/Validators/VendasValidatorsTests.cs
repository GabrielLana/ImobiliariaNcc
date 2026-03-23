using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Vendas.Commands;
using ImobiliariaNcc.Application.Modules.Vendas.Queries;
using ImobiliariaNcc.Application.Modules.Vendas.Validator;

namespace ImobiliariaNcc.Tests.Validators;

public class VendasValidatorTests
{
    private readonly CreateVendaValidator _createValidator = new();
    private readonly UpdateVendaCommandValidator _updateValidator = new();
    private readonly DeleteVendaCommandValidator _deleteValidator = new();
    private readonly GetVendaQueryValidator _getQueryValidator = new();

    [Fact]
    public void Create_DadosValidos_NaoDeveTerErros()
    {
        var command = new CreateVendaCommand(IdCliente: 1, IdApartamento: 1, IdVendedor: 1);
        _createValidator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_IdsInvalidos_DeveFalhar(int idInvalido)
    {
        var command = new CreateVendaCommand(idInvalido, idInvalido, idInvalido);
        var result = _createValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.IdCliente);
        result.ShouldHaveValidationErrorFor(x => x.IdApartamento);
        result.ShouldHaveValidationErrorFor(x => x.IdVendedor);
    }

    [Fact]
    public void Update_DadosValidos_NaoDeveTerErros()
    {
        var command = new UpdateVendaCommand(Id: 1, IdApartamento: 1, IdCliente: 1, IdVendedor: 1);
        _updateValidator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DeleteEGet_IdInvalido_DeveFalhar(int idInvalido)
    {
        _deleteValidator.TestValidate(new DeleteVendaCommand(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
        _getQueryValidator.TestValidate(new GetVendaQuery(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
    }
}