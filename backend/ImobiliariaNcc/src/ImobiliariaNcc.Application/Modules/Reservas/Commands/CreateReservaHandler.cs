using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public class CreateReservaHandler(IReservasRepository _reservaRepository, IApartamentosRepository _apartamentoRepository, IUnitOfWork _uow)
    : IRequestHandler<CreateReservaCommand, int>
{
    public async Task<int> Handle(CreateReservaCommand request, CancellationToken ct)
    {
        var apartamento = await _apartamentoRepository.Get(request.IdApartamento, ct);
        if(apartamento == null)
            throw new NotFoundException("Apartamento não encontrado");

        if (apartamento.Ocupado)
            throw new BadRequestException("Apartamento já ocupado");

        var clientPossuiReserva = await _reservaRepository.ClientePossuiReservaAtiva(request.IdCliente, ct);
        if (clientPossuiReserva)
            throw new BadRequestException("Cliente já possui reserva ativa");

        var apartamentoPossuiReserva = await _reservaRepository.ApartamentoPossuiReservaAtiva(request.IdApartamento, ct);
        if (apartamentoPossuiReserva)
            throw new BadRequestException("Apartamento já possui reserva ativa");

        var reserva = new ReservaModel(request.IdCliente, request.IdApartamento);

        _reservaRepository.Create(reserva);
        await _uow.CommitAsync(ct);

        return reserva.Id;
    }
}