using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Reservas.Commands;

namespace ImobiliariaNcc.Application.Modules.Reservas.Validator;

public class CreateReservaValidator : AbstractValidator<CreateReservaCommand>
{
    public CreateReservaValidator()
    {
        RuleFor(x => x.IdCliente).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.IdApartamento).Obrigatorio().MaiorQue(0);
    }
}
