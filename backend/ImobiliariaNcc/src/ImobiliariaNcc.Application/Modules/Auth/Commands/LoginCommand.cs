using ImobiliariaNcc.Application.Common.Responses;
using MediatR;

namespace ImobiliariaNcc.Application.Modules.Auth.Commands;

public record LoginCommand(string Cpf, string Senha) : IRequest<LoginResponse>;
