using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Queries;

public class GetVendaHandler(IVendasRepository _repository) : IRequestHandler<GetVendaQuery, VendaDto?>
{
    public async Task<VendaDto?> Handle(GetVendaQuery command, CancellationToken ct)
    {
        var result = await _repository.Get(command.Id, ct);
        return result != null ? new VendaDto
        {
            Id = result.Id,
            IdCliente = result.IdCliente,
            IdApartamento = result.IdApartamento,
            IdVendedor = result.IdVendedor,
        } : null;
    }
}