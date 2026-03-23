using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public record UpdateVendaCommand(
    int Id,
    int IdApartamento,
    int IdCliente,
    int IdVendedor
    ) : IRequest;