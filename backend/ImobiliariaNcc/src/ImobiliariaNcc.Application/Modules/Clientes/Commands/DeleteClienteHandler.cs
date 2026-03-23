using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public class DeleteClienteHandler(IClientesRepository _repository, IUnitOfWork _uow) : IRequestHandler<DeleteClienteCommand>
{
    public async Task Handle(DeleteClienteCommand command, CancellationToken ct)
    {
        var cliente = await _repository.Get(command.Id, ct);
        if (cliente == null)
            throw new NotFoundException("Cliente não encontrado");

        _repository.Delete(cliente);
        await _uow.CommitAsync(ct);
    }
}