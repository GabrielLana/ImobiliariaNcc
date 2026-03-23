using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public record GetAllApartamentosQuery() : IRequest<List<ApartamentoDto>>;