using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendas.Queries;

namespace ImobiliariaNcc.Application.Modules.Vendas.Validator;

public class GetVendaQueryValidator : AbstractValidator<GetVendaQuery>
{
    public GetVendaQueryValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}