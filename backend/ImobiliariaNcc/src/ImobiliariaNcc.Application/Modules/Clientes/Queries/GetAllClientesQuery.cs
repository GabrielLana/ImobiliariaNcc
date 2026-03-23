using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Queries;

public record GetAllClientesQuery() : IRequest<List<ClienteDto>>;