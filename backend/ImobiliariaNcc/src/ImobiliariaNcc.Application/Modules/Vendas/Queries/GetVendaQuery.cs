using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Queries;

public record GetVendaQuery(int Id) : IRequest<VendaDto?>;