using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class ReservasRepository : BaseRepository<ReservaModel>, IReservasRepository
{
    public ReservasRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<bool> ClientePossuiReservaAtiva(int idCliente, CancellationToken ct)
        => await _context.Set<ReservaModel>().FirstOrDefaultAsync(x => x.Ativo && x.IdCliente == idCliente, ct) != null;

    public async Task<bool> ApartamentoPossuiReservaAtiva(int idApartamento, CancellationToken ct)
        => await _context.Set<ReservaModel>().FirstOrDefaultAsync(x => x.Ativo && x.IdApartamento == idApartamento, ct) != null;

    public async Task<ReservaModel?> GetReservaAtiva(int idCliente, int idApartamento, CancellationToken ct)
        => await _context.Set<ReservaModel>().FirstOrDefaultAsync(x => x.Ativo && x.IdCliente == idCliente && x.IdApartamento == idApartamento, ct);
}
