using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImobiliariaNcc.Infrastructure.Auth;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly int HoursToExpireToken = 8;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(VendedorModel vendedor)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, vendedor.Id.ToString()),
            new Claim(ClaimTypes.Name, vendedor.Nome)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddHours(HoursToExpireToken),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}