using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public record CreateReservaCommand(
    int IdCliente,
    int IdApartamento
) : IRequest<int>;
