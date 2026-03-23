using FluentValidation;

namespace ImobiliariaNcc.Application.Common.ExtensionMethods;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> Obrigatorio<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty()
            .WithMessage("{PropertyName} não pode ser nulo ou vazio");
    }

    public static IRuleBuilderOptions<T, int> MaiorQue<T>(
        this IRuleBuilder<T, int> ruleBuilder, int number)
    {
        return ruleBuilder.GreaterThan(number)
            .WithMessage("{PropertyName} deve ser maior que " + number);
    }
    public static IRuleBuilderOptions<T, int> MaiorQueOuIgualA<T>(
        this IRuleBuilder<T, int> ruleBuilder, int number)
    {
        return ruleBuilder.GreaterThanOrEqualTo(number)
            .WithMessage("{PropertyName} deve ser maior ou igual a " + number);
    }


    public static IRuleBuilderOptions<T, decimal> ValorNaoNegativo<T>(
        this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder.GreaterThanOrEqualTo(0)
            .WithMessage("{PropertyName} deve ser um valor positivo");
    }

    public static IRuleBuilderOptions<T, string> TamanhoMaximo<T>(
        this IRuleBuilder<T, string> ruleBuilder, int max)
    {
        return ruleBuilder.MaximumLength(max)
            .WithMessage("{PropertyName} deve ter no máximo " + max + " caracteres");
    }
}