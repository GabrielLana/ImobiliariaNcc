using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Clientes.Commands;

namespace ImobiliariaNcc.Application.Modules.Clientes.Validator;

public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
{
    public UpdateClienteCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Nome).Obrigatorio().TamanhoMaximo(200);
        RuleFor(x => x.Cpf).Obrigatorio().Length(11).WithMessage("{PropertyName} deve ter 11 dígitos");
        RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} informado não é válido");
        RuleFor(x => x.Celular).Length(11).WithMessage("{PropertyName} deve ter 11 dígitos (DDD + número)");
        RuleFor(x => x.EstadoCivil).Obrigatorio().TamanhoMaximo(20);
        RuleFor(x => x.DataNascimento).Obrigatorio();
    }
}