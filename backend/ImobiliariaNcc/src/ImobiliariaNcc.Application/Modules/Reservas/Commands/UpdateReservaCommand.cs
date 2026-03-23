using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public record UpdateReservaCommand(
    int Id,
    int IdApartamento,
    int IdCliente,
    bool Ativo
    ) : IRequest;