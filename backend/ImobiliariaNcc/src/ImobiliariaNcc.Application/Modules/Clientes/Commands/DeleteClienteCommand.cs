using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public record DeleteClienteCommand(int Id) : IRequest;