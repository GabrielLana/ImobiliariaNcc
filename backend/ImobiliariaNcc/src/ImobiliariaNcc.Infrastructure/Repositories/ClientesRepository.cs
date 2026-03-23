using ImobiliariaNcc.Application;
using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class ClientesRepository : BaseRepository<ClienteModel>, IClientesRepository
{
    public ClientesRepository(AppDbContext context) : base(context)
    {

    }
}
