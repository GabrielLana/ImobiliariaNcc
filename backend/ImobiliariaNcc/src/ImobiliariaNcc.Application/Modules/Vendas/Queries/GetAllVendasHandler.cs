using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Queries;

public class GetAllVendasHandler(IVendasRepository _repository) : IRequestHandler<GetAllVendasQuery, List<VendaDto>>
{
    public async Task<List<VendaDto>> Handle(GetAllVendasQuery command, CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
        return result.Select(x => new VendaDto
        {
            Id = x.Id,
            IdApartamento = x.IdApartamento,
            IdCliente = x.IdCliente,
            IdVendedor = x.IdVendedor
        }).ToList();
    }
}