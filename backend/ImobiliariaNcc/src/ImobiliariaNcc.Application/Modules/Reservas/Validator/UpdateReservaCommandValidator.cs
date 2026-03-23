using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Reservas.Commands;

namespace ImobiliariaNcc.Application.Modules.Reservas.Validator;

public class UpdateReservaCommandValidator : AbstractValidator<UpdateReservaCommand>
{
    public UpdateReservaCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.IdApartamento).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.IdCliente).Obrigatorio().MaiorQue(0);
    }
}