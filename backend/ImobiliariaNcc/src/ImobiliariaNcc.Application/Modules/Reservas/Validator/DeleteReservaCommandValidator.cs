using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Reservas.Commands;

namespace ImobiliariaNcc.Application.Modules.Reservas.Validator;

public class DeleteReservaCommandValidator : AbstractValidator<DeleteReservaCommand>
{
    public DeleteReservaCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}