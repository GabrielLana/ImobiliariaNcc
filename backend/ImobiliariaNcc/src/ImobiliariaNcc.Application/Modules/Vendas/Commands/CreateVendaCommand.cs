using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendas.Commands;

public record CreateVendaCommand(
    int IdCliente,
    int IdApartamento,
    int IdVendedor
) : IRequest<int>;