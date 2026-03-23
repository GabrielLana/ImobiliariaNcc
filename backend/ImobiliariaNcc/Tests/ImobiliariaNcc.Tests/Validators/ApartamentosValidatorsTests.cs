using AutoFixture;
using Bogus;
using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Apartamentos.Commands;
using ImobiliariaNcc.Application.Modules.Apartamentos.Queries;
using ImobiliariaNcc.Application.Modules.Apartamentos.Validator;

namespace ImobiliariaNcc.Tests.Validators;

public class ApartamentosValidatorsTests
{
    private readonly CreateApartamentoCommandValidator _createValidator;
    private readonly UpdateApartamentoCommandValidator _updateValidator;
    private readonly DeleteApartamentoCommandValidator _deleteValidator;
    private readonly GetApartamentoQueryValidator _getQueryValidator;
    private readonly Faker _faker;
    private readonly Fixture _fixture;

    public ApartamentosValidatorsTests()
    {
        _createValidator = new CreateApartamentoCommandValidator();
        _updateValidator = new UpdateApartamentoCommandValidator();
        _deleteValidator = new DeleteApartamentoCommandValidator();
        _getQueryValidator = new GetApartamentoQueryValidator();
        _faker = new Faker("pt_BR");
        _fixture = new Fixture();
    }

    #region CreateApartamentoCommand
    [Fact]
    public void Create_DadosValidos_NaoDeveTerErros()
    {
        var command = CriarCreateCommandValido();
        var result = _createValidator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_CamposNumericos_DevemSerMaioresQueZero(int valorInvalido)
    {
        var command = CriarCreateCommandValido() with
        {
            Metragem = valorInvalido,
            Quartos = valorInvalido,
            Banheiros = valorInvalido,
            ValorVenda = valorInvalido
        };

        var result = _createValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Metragem);
        result.ShouldHaveValidationErrorFor(x => x.Quartos);
        result.ShouldHaveValidationErrorFor(x => x.Banheiros);
        result.ShouldHaveValidationErrorFor(x => x.ValorVenda);
    }
    #endregion

    #region UpdateApartamentoCommand
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
    public void Update_CepComTamanhoIncorreto_DeveFalhar(string cepInvalido)
    {
        var command = CriarUpdateCommandValido() with { Cep = cepInvalido };
        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Cep)
              .WithErrorMessage("Cep deve ter exatamente 8 caracteres");
    }

    [Fact]
    public void Update_StringsExcedendoLimite_DeveFalhar()
    {
        var command = CriarUpdateCommandValido() with
        {
            Logradouro = new string('A', 151),
            Bairro = new string('A', 101),
            Numero = "123456"
        };

        var result = _updateValidator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Logradouro);
        result.ShouldHaveValidationErrorFor(x => x.Bairro);
        result.ShouldHaveValidationErrorFor(x => x.Numero);
    }
    #endregion

    #region Delete e Get
    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void DeleteEGet_IdInvalido_DeveFalhar(int idInvalido)
    {
        var deleteCmd = new DeleteApartamentoCommand(idInvalido);
        var getQuery = new GetApartamentoQuery(idInvalido);

        _deleteValidator.TestValidate(deleteCmd).ShouldHaveValidationErrorFor(x => x.Id);
        _getQueryValidator.TestValidate(getQuery).ShouldHaveValidationErrorFor(x => x.Id);
    }
    #endregion

    #region Helpers (Geradores de Dados)
    private CreateApartamentoCommand CriarCreateCommandValido()
    {
        return new CreateApartamentoCommand(
            Metragem: _faker.Random.Int(50, 200),
            Quartos: _faker.Random.Int(1, 4),
            Banheiros: _faker.Random.Int(1, 3),
            Vagas: _faker.Random.Int(1, 2),
            DetalhesApartamento: _faker.Commerce.ProductDescription(),
            DetalhesCondominio: _faker.Lorem.Sentence(),
            Andar: _faker.Random.Int(1, 20),
            Bloco: _faker.Random.Int(1, 10),
            ValorVenda: _faker.Random.Decimal(200000, 1000000),
            ValorCondominio: _faker.Random.Decimal(200, 1000),
            ValorIptu: _faker.Random.Decimal(100, 500),
            Cep: "12345678",
            Logradouro: _faker.Address.StreetName(),
            Bairro: _faker.Random.String(30),
            Numero: "123",
            Estado: "SP",
            Cidade: _faker.Address.City(),
            Complemento: null
        );
    }

    private UpdateApartamentoCommand CriarUpdateCommandValido()
    {
        var create = CriarCreateCommandValido();
        return new UpdateApartamentoCommand(
            1,
            false,
            create.Metragem, create.Quartos, create.Banheiros, create.Vagas,
            create.DetalhesApartamento, create.DetalhesCondominio, create.Andar,
            create.Bloco, create.ValorVenda, create.ValorCondominio, create.ValorIptu,
            create.Cep, create.Logradouro, create.Bairro, create.Numero,
            create.Estado, create.Cidade, create.Complemento
        );
    }
    #endregion
}