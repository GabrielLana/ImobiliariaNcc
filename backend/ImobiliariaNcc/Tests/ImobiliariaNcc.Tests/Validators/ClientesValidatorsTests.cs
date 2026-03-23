using Bogus;
using Bogus.Extensions.Brazil;
using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Clientes.Commands;
using ImobiliariaNcc.Application.Modules.Clientes.Queries;
using ImobiliariaNcc.Application.Modules.Clientes.Validator;

namespace ImobiliariaNcc.Tests.Validators;

public class ClienteValidatorTests
{
    private readonly CreateClienteCommandValidator _createValidator;
    private readonly UpdateClienteCommandValidator _updateValidator;
    private readonly DeleteClienteCommandValidator _deleteValidator;
    private readonly GetClienteQueryValidator _getQueryValidator;
    private readonly Faker _faker;

    public ClienteValidatorTests()
    {
        _createValidator = new CreateClienteCommandValidator();
        _updateValidator = new UpdateClienteCommandValidator();
        _deleteValidator = new DeleteClienteCommandValidator();
        _getQueryValidator = new GetClienteQueryValidator();
        _faker = new Faker("pt_BR");
    }

    #region CreateClienteCommand

    [Fact]
    public void Create_DadosValidos_NaoDeveTerErros()
    {
        var command = CriarCreateCommandValido();
        var result = _createValidator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("email_invalido")]
    [InlineData("joao@")]
    [InlineData("@dominio.com")]
    public void Create_EmailInvalido_DeveFalhar(string emailInvalido)
    {
        var command = CriarCreateCommandValido() with { Email = emailInvalido };
        var result = _createValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("Email informado não é válido");
    }

    [Fact]
    public void Create_CpfComTamanhoErrado_DeveFalhar()
    {
        var command = CriarCreateCommandValido() with { Cpf = "1234567890" }; // 10 dígitos
        var result = _createValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cpf);
    }

    #endregion

    #region UpdateClienteCommand

    [Fact]
    public void Update_DadosValidos_NaoDeveTerErros()
    {
        var command = CriarUpdateCommandValido();
        var result = _updateValidator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Update_IdInvalido_DeveFalhar()
    {
        var command = CriarUpdateCommandValido() with { Id = 0 };
        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Update_NomeExcedendoLimite_DeveFalhar()
    {
        var command = CriarUpdateCommandValido() with { Nome = new string('A', 201) };
        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Nome);
    }

    [Fact]
    public void Update_CelularComTamanhoErrado_DeveFalhar()
    {
        var command = CriarUpdateCommandValido() with { Celular = "1199999888" }; // 10 dígitos
        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Celular);
    }

    #endregion

    #region Delete e Get

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DeleteEGet_IdInvalido_DeveFalhar(int idInvalido)
    {
        var deleteCmd = new DeleteClienteCommand(idInvalido);
        var getQuery = new GetClienteQuery(idInvalido);

        _deleteValidator.TestValidate(deleteCmd).ShouldHaveValidationErrorFor(x => x.Id);
        _getQueryValidator.TestValidate(getQuery).ShouldHaveValidationErrorFor(x => x.Id);
    }

    #endregion

    #region Helpers

    private CreateClienteCommand CriarCreateCommandValido()
    {
        return new CreateClienteCommand(
            Nome: _faker.Person.FullName,
            Cpf: _faker.Person.Cpf(false),
            DataNascimento: _faker.Person.DateOfBirth,
            Email: _faker.Internet.Email(),
            Celular: "11999998888",
            EstadoCivil: "Casado"
        );
    }

    private UpdateClienteCommand CriarUpdateCommandValido()
    {
        var create = CriarCreateCommandValido();
        return new UpdateClienteCommand(
            Id: 1,
            Nome: create.Nome,
            Cpf: create.Cpf,
            DataNascimento: create.DataNascimento,
            Email: create.Email,
            Celular: create.Celular,
            EstadoCivil: create.EstadoCivil,
            Ativo: true
        );
    }

    #endregion
}