using Bogus;
using Bogus.Extensions.Brazil;
using FluentValidation.TestHelper;
using ImobiliariaNcc.Application.Modules.Auth.Commands;
using ImobiliariaNcc.Application.Modules.Auth.Validator;

namespace ImobiliariaNcc.Tests.Validators;

public class AuthValidatorsTests
{
    private readonly LoginValidator _validator;
    private readonly Faker _faker;

    public AuthValidatorsTests()
    {
        _validator = new LoginValidator();
        _faker = new Faker("pt_BR");
    }

    [Fact]
    public void Validate_DadosDeLoginValidos_NaoDeveTerErros()
    {
        var command = new LoginCommand(
            _faker.Person.Cpf(false),
            _faker.Internet.Password(8)
        );

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234567890")]
    [InlineData("123456789012")]
    public void Validate_CpfInvalido_DeveTerErro(string cpfInvalido)
    {
        var command = new LoginCommand(cpfInvalido, "senha123");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Cpf);
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345")]
    public void Validate_SenhaInvalida_DeveTerErro(string senhaInvalida)
    {
        var command = new LoginCommand(_faker.Person.Cpf(false), senhaInvalida);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Senha)
              .WithErrorMessage("Senha deve ter no mínimo 6 caracteres");
    }
}