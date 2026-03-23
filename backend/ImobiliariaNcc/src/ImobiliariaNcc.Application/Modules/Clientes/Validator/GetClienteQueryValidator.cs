using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Clientes.Queries;

namespace ImobiliariaNcc.Application.Modules.Clientes.Validator;

public class GetClienteQueryValidator : AbstractValidator<GetClienteQuery>
{
    public GetClienteQueryValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}