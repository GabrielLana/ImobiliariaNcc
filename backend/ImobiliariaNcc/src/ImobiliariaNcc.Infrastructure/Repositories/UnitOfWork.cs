using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Infrastructure.Context;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync();
    }
}