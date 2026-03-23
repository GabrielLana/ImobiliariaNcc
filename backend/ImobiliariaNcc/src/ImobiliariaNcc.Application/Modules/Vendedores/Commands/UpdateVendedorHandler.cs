using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public class UpdateVendedorHandler(IVendedoresRepository _repository, IUnitOfWork _uow) : IRequestHandler<UpdateVendedorCommand>
{
    public async Task Handle(UpdateVendedorCommand command, CancellationToken ct)
    {
        var vendedor = await _repository.Get(command.Id, ct);
        if (vendedor == null)
            throw new NotFoundException("Vendedor não encontrado");

        vendedor.Atualizar(command.Nome, command.Cpf, command.DataNascimento, command.Email, command.Celular, command.Cep, command.Logradouro, command.Bairro,
            command.Numero, command.Complemento, command.Setor, command.NumeroRegistro, command.Ativo);

        _repository.Update(vendedor);
        await _uow.CommitAsync(ct);
    }
}