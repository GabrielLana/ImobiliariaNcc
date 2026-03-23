using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public class DeleteVendedorHandler(IVendedoresRepository _repository, IUnitOfWork _uow) : IRequestHandler<DeleteVendedorCommand>
{
    public async Task Handle(DeleteVendedorCommand command, CancellationToken ct)
    {
        var vendedor = await _repository.Get(command.Id, ct);
        if (vendedor == null)
            throw new NotFoundException("Vendedor não encontrado");

        _repository.Delete(vendedor);
        await _uow.CommitAsync(ct);
    }
}