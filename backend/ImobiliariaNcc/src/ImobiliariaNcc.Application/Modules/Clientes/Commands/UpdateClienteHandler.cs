using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public class UpdateClienteHandler(IClientesRepository _repository, IUnitOfWork _uow) : IRequestHandler<UpdateClienteCommand>
{
    public async Task Handle(UpdateClienteCommand command, CancellationToken ct)
    {
        var cliente = await _repository.Get(command.Id, ct);
        if (cliente == null)
            throw new NotFoundException("Cliente não encontrado");

        cliente.Atualizar(command.Nome, command.Cpf, command.DataNascimento, command.Email, command.Celular, command.EstadoCivil, command.Ativo);

        _repository.Update(cliente);
        await _uow.CommitAsync(ct);
    }
}