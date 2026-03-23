using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public class CreateClienteHandler(IClientesRepository _repo, IUnitOfWork _uow) : IRequestHandler<CreateClienteCommand, int>
{
    public async Task<int> Handle(CreateClienteCommand request, CancellationToken ct)
    {
        var cliente = new ClienteModel(
            request.Nome,
            request.Cpf,
            request.DataNascimento,
            request.Email,
            request.Celular,
            request.EstadoCivil
        );

        _repo.Create(cliente);
        await _uow.CommitAsync(ct);

        return cliente.Id;
    }
}
