using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public class CreateVendaHandler(IVendasRepository _vendaRepository, IReservasRepository _reservaRepository, IApartamentosRepository _apartamentoRepository,
    IUnitOfWork _uow) : IRequestHandler<CreateVendaCommand, int>
{
    public async Task<int> Handle(CreateVendaCommand request, CancellationToken ct)
    {
        var reserva = await _reservaRepository.GetReservaAtiva(request.IdCliente, request.IdApartamento, ct);
        if (reserva == null)
            throw new NotFoundException("Reserva não encontrada");

        var apartamento = await _apartamentoRepository.Get(request.IdApartamento, ct);
        if (apartamento == null)
            throw new NotFoundException("Apartamento não encontrado");

        apartamento.MarcarComoOcupado();
        reserva.Desativar();

        var venda = new VendaModel(
            request.IdCliente,
            request.IdApartamento,
            request.IdVendedor);

        _apartamentoRepository.Update(apartamento);
        _reservaRepository.Update(reserva);
        _vendaRepository.Create(venda);
        await _uow.CommitAsync(ct);

        return venda.Id;
    }
}