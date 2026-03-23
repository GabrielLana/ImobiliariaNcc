using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public class GetApartamentoHandler(IApartamentosRepository _repository) : IRequestHandler<GetApartamentoQuery, ApartamentoDto?>
{
    public async Task<ApartamentoDto?> Handle(GetApartamentoQuery command, CancellationToken ct)
    {
        var result = await _repository.Get(command.Id, ct);
        return result != null ? new ApartamentoDto
        {
            Id = result.Id,
            Andar = result.Andar,
            Bairro = result.Bairro,
            Banheiros = result.Banheiros,
            Bloco = result.Bloco,
            Cep = result.Cep,
            Complemento = result.Complemento,
            Logradouro = result.Logradouro,
            Metragem = result.Metragem,
            Numero = result.Numero,
            DetalhesCondominio = result.DetalhesCondominio,
            DetalhesApartamento = result.DetalhesApartamento,
            Ocupado = result.Ocupado,
            Quartos = result.Quartos,
            Vagas = result.Vagas,
            ValorCondominio = result.ValorCondominio,
            ValorIptu = result.ValorIptu,
            ValorVenda = result.ValorVenda,
            Estado = result.Estado,
            Cidade = result.Cidade
        } : null;
    }
}