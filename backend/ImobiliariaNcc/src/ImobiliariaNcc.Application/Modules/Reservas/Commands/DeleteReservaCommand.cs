using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Commands;

public record DeleteReservaCommand(int Id) : IRequest;