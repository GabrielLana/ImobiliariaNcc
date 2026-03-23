using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Queries;

public record GetVendedorQuery(int Id) : IRequest<VendedorDto?>;