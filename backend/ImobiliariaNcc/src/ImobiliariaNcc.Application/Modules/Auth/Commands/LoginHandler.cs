using ImobiliariaNcc.Application.Common.Responses;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Exceptions;
using ImobiliariaNcc.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ImobiliariaNcc.Application.Modules.Auth.Commands;

public class LoginHandler(IVendedoresRepository _repo, IJwtService _jwt, IPasswordHasher<VendedorModel> _passwordHasher) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken ct)
    {
        var vendedor = await _repo.GetByCpf(request.Cpf, ct);

        if (vendedor == null)
            throw new UnauthorizedException("Credenciais inválidas");

        var result = _passwordHasher.VerifyHashedPassword(vendedor, vendedor.Senha, request.Senha);
        if (result == PasswordVerificationResult.Failed)
            throw new UnauthorizedException("Credenciais inválidas");

        var token = _jwt.GenerateToken(vendedor);

        return new LoginResponse
        {
            Token = token,
            Nome = vendedor.Nome
        };
    }
}
