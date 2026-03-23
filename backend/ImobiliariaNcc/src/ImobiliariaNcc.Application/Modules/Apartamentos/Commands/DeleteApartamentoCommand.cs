using MediatR;

namespace ImobiliariaNcc.Application.Modules.Apartamentos.Commands;

public record DeleteApartamentoCommand(int Id) : IRequest;