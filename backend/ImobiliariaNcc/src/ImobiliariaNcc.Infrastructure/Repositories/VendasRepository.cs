using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class VendasRepository : BaseRepository<VendaModel>, IVendasRepository
{
    public VendasRepository(AppDbContext context) : base(context)
    {

    }
}
