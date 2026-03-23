using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Application.Interfaces.Repositories;

public interface IVendedoresRepository : IBaseRepository<VendedorModel>
{
    Task<VendedorModel?> GetByCpf(string cpf, CancellationToken ct);
}
