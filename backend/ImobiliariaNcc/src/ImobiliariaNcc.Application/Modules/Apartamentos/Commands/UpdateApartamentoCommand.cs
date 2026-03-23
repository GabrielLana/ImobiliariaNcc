using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

public record UpdateApartamentoCommand(
        int Id,
        bool Ocupado,
        int Metragem,
        int Quartos,
        int Banheiros,
        int Vagas,
        string DetalhesApartamento,
        string DetalhesCondominio,
        int Andar,
        int Bloco,
        decimal ValorVenda,
        decimal ValorCondominio,
        decimal ValorIptu,
        string Cep,
        string Logradouro,
        string Bairro,
        string Numero,
        string Estado,
        string Cidade,
        string? Complemento
    ) : IRequest;