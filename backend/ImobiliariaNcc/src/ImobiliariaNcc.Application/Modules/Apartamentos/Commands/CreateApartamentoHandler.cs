using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

public class CreateApartamentoHandler(IApartamentosRepository _repository, IUnitOfWork _uow) : IRequestHandler<CreateApartamentoCommand, int>
{
    public async Task<int> Handle(CreateApartamentoCommand command, CancellationToken ct)
    {
        var apartamento = new ApartamentoModel(command.Metragem, command.Quartos, command.Banheiros, command.Vagas, command.DetalhesApartamento, 
            command.DetalhesCondominio, command.Andar, command.Bloco, command.ValorVenda, command.ValorCondominio, command.ValorIptu, command.Cep,
            command.Logradouro, command.Bairro, command.Numero, command.Estado, command.Cidade, command.Complemento);

        _repository.Create(apartamento);
        await _uow.CommitAsync(ct);

        return apartamento.Id;
    }
}
