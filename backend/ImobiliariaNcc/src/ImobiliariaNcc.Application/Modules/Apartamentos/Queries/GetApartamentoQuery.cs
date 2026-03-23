using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public record GetApartamentoQuery(int Id) : IRequest<ApartamentoDto?>;