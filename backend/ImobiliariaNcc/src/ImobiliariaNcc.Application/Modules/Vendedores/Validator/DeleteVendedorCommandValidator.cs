using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Vendedores.Commands;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Validator;

public class DeleteVendedorCommandValidator : AbstractValidator<DeleteVendedorCommand>
{
    public DeleteVendedorCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
    }
}