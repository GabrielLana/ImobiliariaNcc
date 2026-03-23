using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Queries;

public record GetClienteQuery(int Id) : IRequest<ClienteDto?>;