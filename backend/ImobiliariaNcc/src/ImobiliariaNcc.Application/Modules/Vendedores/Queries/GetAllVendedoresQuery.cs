using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Queries;

public record GetAllVendedoresQuery() : IRequest<List<VendedorDto>>;