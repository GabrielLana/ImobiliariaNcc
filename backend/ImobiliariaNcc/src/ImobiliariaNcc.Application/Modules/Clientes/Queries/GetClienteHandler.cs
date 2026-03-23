using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Queries;

public class GetClienteHandler(IClientesRepository _repository) : IRequestHandler<GetClienteQuery, ClienteDto?>
{
    public async Task<ClienteDto?> Handle(GetClienteQuery command, CancellationToken ct)
    {
        var result = await _repository.Get(command.Id, ct);
        return result != null ? new ClienteDto
        {
            Id = result.Id,
            Nome = result.Nome,
            EstadoCivil = result.EstadoCivil,
            Email = result.Email,
            DataNascimento = result.DataNascimento,
            Cpf = result.Cpf,
            Celular = result.Celular,
            Ativo = result.Ativo
        } : null;
    }
}