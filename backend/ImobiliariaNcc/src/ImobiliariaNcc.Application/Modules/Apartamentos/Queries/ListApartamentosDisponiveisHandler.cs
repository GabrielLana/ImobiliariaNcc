using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public class ListApartamentosDisponiveisHandler(IApartamentosRepository _apartamentoRepository, IReservasRepository _reservaRepository)
    : IRequestHandler<ListApartamentosDisponiveisQuery, List<ApartamentoComDisponibilidadeDto>>
{
    public async Task<List<ApartamentoComDisponibilidadeDto>> Handle(ListApartamentosDisponiveisQuery request, CancellationToken cancellationToken)
    {
        var apartamentos = await _apartamentoRepository.GetAll(cancellationToken);
        var reservas = await _reservaRepository.GetAll(cancellationToken);
        return apartamentos.Where(x => !x.Ocupado).Select(x => new ApartamentoComDisponibilidadeDto
        {
            Id = x.Id,
            Andar = x.Andar,
            Bairro = x.Bairro,
            Banheiros = x.Banheiros,
            Bloco = x.Bloco,
            Cep = x.Cep,
            Complemento = x.Complemento,
            DetalhesApartamento = x.DetalhesApartamento,
            DetalhesCondominio = x.DetalhesCondominio,
            Logradouro = x.Logradouro,
            Metragem = x.Metragem,
            Numero = x.Numero,
            Ocupado = x.Ocupado,
            Quartos = x.Quartos,
            Vagas = x.Vagas,
            ValorCondominio = x.ValorCondominio,
            ValorIptu = x.ValorIptu,
            ValorVenda = x.ValorVenda,
            Estado = x.Estado,
            Cidade = x.Cidade,
            Reservado = reservas.Any(y => y.Ativo && y.IdApartamento == x.Id)
        }).ToList();
    }
}