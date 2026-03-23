using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Reservas.Queries;

namespace ImobiliariaNcc.Application.Modules.Reservas.Validator;

public class GetReservaQueryValidator : AbstractValidator<GetReservaQuery>
{
    public GetReservaQueryValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}