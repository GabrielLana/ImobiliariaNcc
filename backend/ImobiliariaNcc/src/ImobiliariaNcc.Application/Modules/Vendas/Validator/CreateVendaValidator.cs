using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendas.Commands;

namespace ImobiliariaNcc.Application.Modules.Vendas.Validator;

public class CreateVendaValidator : AbstractValidator<CreateVendaCommand>
{
    public CreateVendaValidator()
    {
        RuleFor(x => x.IdCliente).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.IdApartamento).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.IdVendedor).Obrigatorio().MaiorQue(0);
    }
}