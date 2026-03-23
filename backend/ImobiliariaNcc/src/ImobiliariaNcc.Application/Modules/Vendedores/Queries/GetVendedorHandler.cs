using ImobiliariaNcc.Application.Common.DTO;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Queries;

public class GetVendedorHandler(IVendedoresRepository _repository) : IRequestHandler<GetVendedorQuery, VendedorDto?>
{
    public async Task<VendedorDto?> Handle(GetVendedorQuery command, CancellationToken ct)
    {
        var result = await _repository.Get(command.Id, ct);
        return result != null ? new VendedorDto
        {
            Id = result.Id,
            Complemento = result.Complemento,
            Bairro = result.Bairro,
            Logradouro = result.Logradouro,
            Cep = result.Cep,
            Nome = result.Nome,
            Celular = result.Celular,
            Ativo = result.Ativo,
            Cpf = result.Cpf,
            DataNascimento = result.DataNascimento,
            Email = result.Email,
            Setor = result.Setor,
            Numero = result.Numero,
            NumeroRegistro = result.NumeroRegistro,
        } : null;
    }
}