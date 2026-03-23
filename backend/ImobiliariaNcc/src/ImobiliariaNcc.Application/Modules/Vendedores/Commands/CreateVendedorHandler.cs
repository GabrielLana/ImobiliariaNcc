using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public class CreateVendedorHandler(IVendedoresRepository _repository, IUnitOfWork _uow, IPasswordHasher<VendedorModel> _passwordHasher) : IRequestHandler<CreateVendedorCommand, int>
{
    public async Task<int> Handle(CreateVendedorCommand command, CancellationToken ct)
    {
        var vendedor = new VendedorModel(
            command.Nome, command.Cpf, "", command.DataNascimento, command.Email, command.Celular, command.Cep, command.Logradouro, command.Bairro,
            command.Numero, command.Complemento, command.Setor, command.NumeroRegistro);

        var hash = _passwordHasher.HashPassword(vendedor, command.Senha);
        vendedor.AtualizaSenha(hash);

        _repository.Create(vendedor);
        await _uow.CommitAsync(ct);

        return vendedor.Id;
    }
}