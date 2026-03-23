using MediatR;

namespace ImobiliariaNcc.Application.Modules.Vendedores.Commands;

public record UpdateVendedorCommand(
    int Id,
    bool Ativo,
    string Nome,
    string Cpf,
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
    ) : IRequest;