using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public class UpdateReservaHandler(IReservasRepository _repository, IUnitOfWork _uow) : IRequestHandler<UpdateReservaCommand>
{
    public async Task Handle(UpdateReservaCommand command, CancellationToken ct)
    {
        var reserva = await _repository.Get(command.Id, ct);
        if (reserva == null)
            throw new NotFoundException("Reserva não encontrada");

        reserva.Atualizar(command.Ativo, command.IdCliente, command.IdApartamento);

        _repository.Update(reserva);
        await _uow.CommitAsync(ct);
    }
}