using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

public class UpdateApartamentoHandler(IApartamentosRepository _repository, IUnitOfWork _uow) : IRequestHandler<UpdateApartamentoCommand>
{
    public async Task Handle(UpdateApartamentoCommand command, CancellationToken ct)
    {
        var apartamento = await _repository.Get(command.Id, ct);
        if (apartamento == null)
            throw new NotFoundException("Apartamento não encontrado");

        apartamento.Atualizar(command.Ocupado, command.Metragem, command.Quartos, command.Banheiros, command.Vagas, command.DetalhesApartamento,
            command.DetalhesCondominio, command.Andar, command.Bloco, command.ValorVenda, command.ValorCondominio, command.ValorIptu, command.Cep,
            command.Logradouro, command.Bairro, command.Numero, command.Estado, command.Cidade, command.Complemento);

        _repository.Update(apartamento);
        await _uow.CommitAsync(ct);
    }
}