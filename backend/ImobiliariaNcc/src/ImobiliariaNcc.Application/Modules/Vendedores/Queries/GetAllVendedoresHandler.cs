using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Queries;

public class GetAllVendedoresHandler(IVendedoresRepository _repository) : IRequestHandler<GetAllVendedoresQuery, List<VendedorDto>>
{
    public async Task<List<VendedorDto>> Handle(GetAllVendedoresQuery command, CancellationToken ct)
    {
        var result = await _repository.GetAll(ct);
        return result.Select(x => new VendedorDto
        {
            Id = x.Id,
            Complemento = x.Complemento,
            Bairro = x.Bairro,
            Logradouro = x.Logradouro,
            Cep = x.Cep,
            Nome = x.Nome,
            Celular = x.Celular,
            Ativo = x.Ativo,
            Cpf = x.Cpf,
            DataNascimento = x.DataNascimento,
            Email = x.Email,
            Setor = x.Setor,
            Numero = x.Numero,
            NumeroRegistro = x.NumeroRegistro
        }).ToList();
    }
}