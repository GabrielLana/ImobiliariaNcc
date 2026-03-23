using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Queries;

public class GetAllClientesHandler(IClientesRepository _repository) : IRequestHandler<GetAllClientesQuery, List<ClienteDto>>
{
    public async Task<List<ClienteDto>> Handle(GetAllClientesQuery command, CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
        return result.Select(x => new ClienteDto
        {
            Id = x.Id,
            Ativo = x.Ativo,
            Celular = x.Celular,
            Cpf = x.Cpf,
            DataNascimento = x.DataNascimento,
            Email = x.Email,
            EstadoCivil = x.EstadoCivil,
            Nome = x.Nome
        }).ToList();
    }
}