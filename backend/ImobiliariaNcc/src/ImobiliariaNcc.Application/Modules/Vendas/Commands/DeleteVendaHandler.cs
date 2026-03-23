using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public class DeleteVendaHandler(IVendasRepository _repository, IUnitOfWork _uow) : IRequestHandler<DeleteVendaCommand>
{
    public async Task Handle(DeleteVendaCommand command, CancellationToken ct)
    {
        var venda = await _repository.Get(command.Id, ct);
        if (venda == null)
            throw new NotFoundException("Venda não encontrada");

        _repository.Delete(venda);
        await _uow.CommitAsync(ct);
    }
}