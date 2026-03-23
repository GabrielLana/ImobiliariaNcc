using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Queries;

public class GetAllReservasHandler(IReservasRepository _repository) : IRequestHandler<GetAllReservasQuery, List<ReservaDto>>
{
    public async Task<List<ReservaDto>> Handle(GetAllReservasQuery command, CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
        return result.Select(x => new ReservaDto
        {
            Id = x.Id,
            Ativo = x.Ativo,
            IdApartamento = x.IdApartamento,
            IdCliente = x.IdCliente
        }).ToList();
    }
}