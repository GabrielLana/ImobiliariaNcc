using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Application.Interfaces.Repositories;

public interface IJwtService
{
    string GenerateToken(VendedorModel vendedor);
}