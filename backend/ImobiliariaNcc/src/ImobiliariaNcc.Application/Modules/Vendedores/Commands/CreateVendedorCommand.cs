using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public record CreateVendedorCommand(
    string Nome,
    string Cpf,
    string Senha,
    DateTime DataNascimento,
    string Email,
    string Celular,
    string Setor,
    int NumeroRegistro,
    string Cep,
    string Logradouro,
    string Bairro,
    string Numero,
    string? Complemento
) : IRequest<int>;