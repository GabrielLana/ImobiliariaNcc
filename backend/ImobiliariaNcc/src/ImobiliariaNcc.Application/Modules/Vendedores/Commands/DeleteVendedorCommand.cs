using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public record DeleteVendedorCommand(int Id) : IRequest;