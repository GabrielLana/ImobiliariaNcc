using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendas.Commands;

namespace ImobiliariaNcc.Application.Modules.Vendas.Validator;

public class DeleteVendaCommandValidator : AbstractValidator<DeleteVendaCommand>
{
    public DeleteVendaCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}