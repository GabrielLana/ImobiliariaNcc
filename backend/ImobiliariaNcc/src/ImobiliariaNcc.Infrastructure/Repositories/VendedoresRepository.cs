using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class VendedoresRepository : BaseRepository<VendedorModel>, IVendedoresRepository
{
    public VendedoresRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<VendedorModel?> GetByCpf(string cpf, CancellationToken ct)
        => await _context.Set<VendedorModel>().FirstOrDefaultAsync(x => x.Cpf == cpf, ct);
}