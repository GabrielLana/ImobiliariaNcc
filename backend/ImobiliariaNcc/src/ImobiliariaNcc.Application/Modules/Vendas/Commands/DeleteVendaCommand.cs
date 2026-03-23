using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public record DeleteVendaCommand(int Id) : IRequest;