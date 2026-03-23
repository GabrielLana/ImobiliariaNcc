using ImobiliariaNcc.Application.Common.DTO;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Queries;

public record ListApartamentosDisponiveisQuery()
    : IRequest<List<ApartamentoComDisponibilidadeDto>>;