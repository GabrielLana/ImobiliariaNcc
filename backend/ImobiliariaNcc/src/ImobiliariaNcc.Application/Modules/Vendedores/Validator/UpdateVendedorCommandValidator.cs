using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendedores.Commands;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Validator;

public class UpdateVendedorCommandValidator : AbstractValidator<UpdateVendedorCommand>
{
    public UpdateVendedorCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);

        RuleFor(x => x.Cpf).Obrigatorio().Length(11)
            .WithMessage("{PropertyName} deve ter 11 dígitos");

        RuleFor(x => x.Email).Obrigatorio().EmailAddress()
            .WithMessage("{PropertyName} inválido");

        RuleFor(x => x.Celular).Length(11)
            .WithMessage("{PropertyName} deve ter 11 dígitos");

        RuleFor(x => x.DataNascimento).Obrigatorio();

        RuleFor(x => x.Cep).Obrigatorio().Length(8)
            .WithMessage("{PropertyName} deve ter 8 caracteres");

        RuleFor(x => x.Logradouro).Obrigatorio().TamanhoMaximo(150);
        RuleFor(x => x.Bairro).Obrigatorio().TamanhoMaximo(100);
        RuleFor(x => x.Numero).Obrigatorio().TamanhoMaximo(5);
        RuleFor(x => x.Complemento).TamanhoMaximo(100);
        RuleFor(x => x.Setor).Obrigatorio().TamanhoMaximo(50);
        RuleFor(x => x.NumeroRegistro).Obrigatorio().MaiorQue(0);
    }
}