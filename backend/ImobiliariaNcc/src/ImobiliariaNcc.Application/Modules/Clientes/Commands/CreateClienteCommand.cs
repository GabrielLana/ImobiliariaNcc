using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public record CreateClienteCommand(
    string Nome,
    string Cpf,
    DateTime DataNascimento,
    string Email,
    string Celular,
    string EstadoCivil
) : IRequest<int>;
