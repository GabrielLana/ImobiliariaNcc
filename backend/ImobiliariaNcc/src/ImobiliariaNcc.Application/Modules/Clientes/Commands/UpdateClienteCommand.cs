using MediatR;

namespace ImobiliariaNcc.Application.Modules.Clientes.Commands;

public record UpdateClienteCommand(
    int Id,
    string Nome,
    string Cpf,
    DateTime DataNascimento,
    string Email,
    string Celular,
    string EstadoCivil,
    bool Ativo
    ) : IRequest;