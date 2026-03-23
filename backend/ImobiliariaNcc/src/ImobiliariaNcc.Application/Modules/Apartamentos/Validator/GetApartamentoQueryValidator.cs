using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Validator;

public class GetApartamentoQueryValidator : AbstractValidator<GetApartamentoQuery>
{
    public GetApartamentoQueryValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}