using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendedores.Queries;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Validator;

public class GetVendedorQueryValidator : AbstractValidator<GetVendedorQuery>
{
    public GetVendedorQueryValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}