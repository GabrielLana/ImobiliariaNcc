using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Auth.Commands;

namespace ImobiliariaNcc.Application.Modules.Auth.Validator;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Cpf).Obrigatorio().Length(11)
            .WithMessage("{PropertyName} deve ter 11 dígitos");

        RuleFor(x => x.Senha).Obrigatorio().MinimumLength(6)
            .WithMessage("{PropertyName} deve ter no mínimo 6 caracteres");
    }
}
