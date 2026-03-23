using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendedores.Commands;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Validator;

public class CreateVendedorValidator : AbstractValidator<CreateVendedorCommand>
{
    public CreateVendedorValidator()
    {
        RuleFor(x => x.Nome).Obrigatorio().TamanhoMaximo(200);

        RuleFor(x => x.Cpf).Obrigatorio().Length(11)
            .WithMessage("{PropertyName} deve ter exatamente 11 dígitos");

        RuleFor(x => x.Senha).Obrigatorio().MinimumLength(6)
            .WithMessage("{PropertyName} deve ter no mínimo 6 caracteres");

        RuleFor(x => x.Email).Obrigatorio().EmailAddress()
            .WithMessage("{PropertyName} informado não é válido");

        RuleFor(x => x.Celular).Length(11)
            .WithMessage("{PropertyName} deve ter 11 dígitos (DDD + número)");

        RuleFor(x => x.DataNascimento).Obrigatorio();

        RuleFor(x => x.Cep).Obrigatorio().Length(8)
            .WithMessage("{PropertyName} deve ter exatamente 8 caracteres");

        RuleFor(x => x.Logradouro).Obrigatorio().TamanhoMaximo(150);
        RuleFor(x => x.Bairro).Obrigatorio().TamanhoMaximo(100);
        RuleFor(x => x.Numero).Obrigatorio().TamanhoMaximo(5);
        RuleFor(x => x.Complemento).TamanhoMaximo(100);
        RuleFor(x => x.Setor).Obrigatorio().TamanhoMaximo(50);
        RuleFor(x => x.NumeroRegistro).Obrigatorio().MaiorQue(0);
    }
}