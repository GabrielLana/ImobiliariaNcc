using FluentValidation;
using ImobiliariaNcc.Application.Common.ExtensionMethods;
using ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Validator;

public class UpdateApartamentoCommandValidator : AbstractValidator<UpdateApartamentoCommand>
{
    public UpdateApartamentoCommandValidator()
    {
        RuleFor(x => x.Id).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Metragem).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Quartos).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Banheiros).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Vagas).Obrigatorio().MaiorQueOuIgualA(0);

        RuleFor(x => x.DetalhesApartamento).Obrigatorio();
        RuleFor(x => x.DetalhesCondominio).Obrigatorio();

        RuleFor(x => x.Andar).Obrigatorio().MaiorQue(0);
        RuleFor(x => x.Bloco).Obrigatorio().MaiorQue(0);

        RuleFor(x => x.ValorVenda).Obrigatorio().GreaterThan(0).WithMessage("{PropertyName} deve ser maior que zero");
        RuleFor(x => x.ValorCondominio).Obrigatorio().ValorNaoNegativo();
        RuleFor(x => x.ValorIptu).Obrigatorio().ValorNaoNegativo();

        RuleFor(x => x.Cep).Obrigatorio().Length(8)
            .WithMessage("{PropertyName} deve ter exatamente 8 caracteres");

        RuleFor(x => x.Logradouro).Obrigatorio().TamanhoMaximo(150);
        RuleFor(x => x.Bairro).Obrigatorio().TamanhoMaximo(100);
        RuleFor(x => x.Numero).Obrigatorio().TamanhoMaximo(5);
        RuleFor(x => x.Estado).Obrigatorio().TamanhoMaximo(50);
        RuleFor(x => x.Cidade).Obrigatorio().TamanhoMaximo(200);
        RuleFor(x => x.Complemento).TamanhoMaximo(100);
    }
}
