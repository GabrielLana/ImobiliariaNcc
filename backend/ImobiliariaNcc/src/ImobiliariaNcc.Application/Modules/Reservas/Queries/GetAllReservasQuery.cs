using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Reservas.Queries;

public record GetAllReservasQuery() : IRequest<List<ReservaDto>>;