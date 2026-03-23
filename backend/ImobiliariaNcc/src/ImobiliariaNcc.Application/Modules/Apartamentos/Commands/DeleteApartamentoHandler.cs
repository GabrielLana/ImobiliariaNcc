using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

public class DeleteApartamentoHandler(IApartamentosRepository _repository, IUnitOfWork _uow) : IRequestHandler<DeleteApartamentoCommand>
{
    public async Task Handle(DeleteApartamentoCommand command, CancellationToken ct)
    {
        var apartamento = await _repository.Get(command.Id, ct);
        if (apartamento == null)
            throw new NotFoundException("Apartamento não encontrado");

        _repository.Delete(apartamento);
        await _uow.CommitAsync(ct);
    }
}