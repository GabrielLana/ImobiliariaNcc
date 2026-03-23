using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public class GetAllApartamentosHandler(IApartamentosRepository _repository) : IRequestHandler<GetAllApartamentosQuery, List<ApartamentoDto>>
{
    public async Task<List<ApartamentoDto>> Handle(GetAllApartamentosQuery command, CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
        return result.Select(x => new ApartamentoDto
        {
            Id = x.Id,
            Andar = x.Andar,
            Bairro = x.Bairro,
            Banheiros = x.Banheiros,
            Bloco = x.Bloco,
            Cep = x.Cep,
            Complemento = x.Complemento,
            Logradouro = x.Logradouro,
            Numero = x.Numero,
            Ocupado = x.Ocupado,
            Vagas = x.Vagas,
            ValorCondominio = x.ValorCondominio,
            ValorIptu = x.ValorIptu,
            DetalhesCondominio = x.DetalhesCondominio,
            DetalhesApartamento = x.DetalhesApartamento,
            Metragem = x.Metragem,
            Quartos = x.Quartos,
            ValorVenda = x.ValorVenda,
            Estado = x.Estado,
            Cidade = x.Cidade
        }).ToList();
    }
}