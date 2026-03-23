using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public class DeleteReservaHandler(IReservasRepository _repository, IUnitOfWork _uow) : IRequestHandler<DeleteReservaCommand>
{
    public async Task Handle(DeleteReservaCommand command, CancellationToken ct)
    {
        var reserva = await _repository.Get(command.Id, ct);
        if (reserva == null)
            throw new NotFoundException("Reserva não encontrada");

        _repository.Delete(reserva);
        await _uow.CommitAsync(ct);
    }
}