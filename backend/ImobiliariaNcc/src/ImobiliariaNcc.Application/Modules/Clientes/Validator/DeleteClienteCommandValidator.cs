using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Clientes.Commands;

namespace ImobiliariaNcc.Application.Modules.Clientes.Validator;

public class DeleteClienteCommandValidator : AbstractValidator<DeleteClienteCommand>
{
    public DeleteClienteCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}