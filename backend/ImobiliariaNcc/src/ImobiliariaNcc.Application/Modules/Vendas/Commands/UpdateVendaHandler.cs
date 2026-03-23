using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public class UpdateVendaHandler(IVendasRepository _repository, IUnitOfWork _uow) : IRequestHandler<UpdateVendaCommand>
{
    public async Task Handle(UpdateVendaCommand command, CancellationToken ct)
    {
        var venda = await _repository.Get(command.Id, ct);
        if (venda == null)
            throw new NotFoundException("Venda não encontrada");

        venda.Atualizar(command.IdCliente, command.IdApartamento, command.IdVendedor);

        _repository.Update(venda);
        await _uow.CommitAsync(ct);
    }
}