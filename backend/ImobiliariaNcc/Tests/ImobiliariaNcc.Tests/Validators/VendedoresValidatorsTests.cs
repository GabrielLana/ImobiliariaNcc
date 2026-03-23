using Bogus;
using Bogus.Extensions.Brazil;
using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Vendedores.Commands;
using ImobiliariaNcc.Application.Modules.Vendedores.Queries;
using ImobiliariaNcc.Application.Modules.Vendedores.Validator;
using Xunit;

namespace ImobiliariaNcc.Tests.Validators;

public class VendedoresValidatorTests
{
    private readonly CreateVendedorValidator _createValidator;
    private readonly UpdateVendedorCommandValidator _updateValidator;
    private readonly DeleteVendedorCommandValidator _deleteValidator;
    private readonly GetVendedorQueryValidator _getQueryValidator;
    private readonly Faker _faker;

    public VendedoresValidatorTests()
    {
        _createValidator = new CreateVendedorValidator();
        _updateValidator = new UpdateVendedorCommandValidator();
        _deleteValidator = new DeleteVendedorCommandValidator();
        _getQueryValidator = new GetVendedorQueryValidator();
        _faker = new Faker("pt_BR");
    }

    #region CreateVendedorCommand

    [Fact]
    public void Create_DadosValidos_NaoDeveTerErros()
    {
        var command = CriarCreateCommandValido();
        var result = _createValidator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("")]
    public void Create_SenhaInvalida_DeveFalhar(string senhaInvalida)
    {
        var command = CriarCreateCommandValido() with { Senha = senhaInvalida };
        var result = _createValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Senha);
    }

    [Fact]
    public void Create_NumeroRegistroInvalido_DeveFalhar()
    {
        var command = CriarCreateCommandValido() with { NumeroRegistro = 0 };
        var result = _createValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NumeroRegistro);
    }

    #endregion

    #region UpdateVendedorCommand

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

    [Theory]
    [InlineData("1234567")]
    [InlineData("123456789")]
    public void Update_CepInvalido_DeveFalhar(string cepInvalido)
    {
        var command = CriarUpdateCommandValido() with { Cep = cepInvalido };
        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cep);
    }

    [Fact]
    public void Update_CamposObrigatoriosVazios_DeveFalhar()
    {
        var command = CriarUpdateCommandValido() with
        {
            Cpf = "",
            Email = "invalido",
            Setor = ""
        };

        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cpf);
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldHaveValidationErrorFor(x => x.Setor);
    }

    #endregion

    #region Delete e Get

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void DeleteEGet_IdInvalido_DeveFalhar(int idInvalido)
    {
        _deleteValidator.TestValidate(new DeleteVendedorCommand(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
        _getQueryValidator.TestValidate(new GetVendedorQuery(idInvalido)).ShouldHaveValidationErrorFor(x => x.Id);
    }

    #endregion

    #region Helpers

    private CreateVendedorCommand CriarCreateCommandValido()
    {
        return new CreateVendedorCommand(
            Nome: _faker.Person.FullName,
            Cpf: _faker.Person.Cpf(false),
            Senha: _faker.Internet.Password(8),
            DataNascimento: _faker.Date.Past(30, DateTime.Now.AddYears(-18)),
            Email: _faker.Internet.Email(),
            Celular: "11988887777",
            Setor: "Vendas Internas",
            NumeroRegistro: _faker.Random.Int(1000, 9999),
            Cep: "01234567",
            Logradouro: _faker.Address.StreetName(),
            Bairro: _faker.Random.String(40),
            Numero: "100",
            Complemento: "Sala 2"
        );
    }

    private UpdateVendedorCommand CriarUpdateCommandValido()
    {
        var create = CriarCreateCommandValido();
        return new UpdateVendedorCommand(
            Id: 1,
            Ativo: true,
            Nome: create.Nome,
            Cpf: create.Cpf,
            DataNascimento: create.DataNascimento,
            Email: create.Email,
            Celular: create.Celular,
            Setor: create.Setor,
            NumeroRegistro: create.NumeroRegistro,
            Cep: create.Cep,
            Logradouro: create.Logradouro,
            Bairro: create.Bairro,
            Numero: create.Numero,
            Complemento: create.Complemento
        );
    }

    #endregion
}