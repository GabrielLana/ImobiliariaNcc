using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Reservas.Commands;
using ImobiliariaNcc.Application.Modules.Reservas.Queries;
using ImobiliariaNcc.Application.Modules.Reservas.Validator;

namespace ImobiliariaNcc.Tests.Validators;

public class ReservasValidatorTests
{
    private readonly CreateReservaValidator _createValidator = new();
    private readonly UpdateReservaCommandValidator _updateValidator = new();
    private readonly DeleteReservaCommandValidator _deleteValidator = new();
    private readonly GetReservaQueryValidator _getQueryValidator = new();

    [Fact]
    public void Create_DadosValidos_NaoDeveTerErros()
    {
        var command = new CreateReservaCommand(IdCliente: 1, IdApartamento: 1);
        _createValidator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_IdsInvalidos_DeveFalhar(int idInvalido)
    {
        var command = new CreateReservaCommand(idInvalido, idInvalido);
        var result = _createValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.IdCliente);
        result.ShouldHaveValidationErrorFor(x => x.IdApartamento);
    }

    [Fact]
    public void Update_DadosValidos_NaoDeveTerErros()
    {
        var command = new UpdateReservaCommand(Id: 1, IdApartamento: 1, IdCliente: 1, Ativo: true);
        _updateValidator.TestValidate(command).ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Update_IdInvalido_DeveFalhar()
    {
        var command = new UpdateReservaCommand(0, 1, 1, true);
        _updateValidator.TestValidate(command).ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DeleteEGet_IdInvalido_DeveFalhar(int idInvalido)
    {
        _deleteValidator.TestValidate(new DeleteReservaCommand(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
        _getQueryValidator.TestValidate(new GetReservaQuery(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
    }
}