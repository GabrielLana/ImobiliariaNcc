using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class ApartamentosRepository : BaseRepository<ApartamentoModel>,IApartamentosRepository
{
    public ApartamentosRepository(AppDbContext context) : base(context)
    {
        
    }
}