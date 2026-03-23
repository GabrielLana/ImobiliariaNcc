using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Validator;

public class DeleteApartamentoCommandValidator : AbstractValidator<DeleteApartamentoCommand>
{
    public DeleteApartamentoCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}