using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Queries;

public class GetReservaHandler(IReservasRepository _repository) : IRequestHandler<GetReservaQuery, ReservaDto?>
{
    public async Task<ReservaDto?> Handle(GetReservaQuery command, CancellationToken ct)
    {
        var result = await _repository.Get(command.Id, ct);
        return result != null ? new ReservaDto
        {
            Id = result.Id,
            Ativo = result.Ativo,
            IdCliente = result.IdCliente,
            IdApartamento = result.IdApartamento
        } : null;
    }
}