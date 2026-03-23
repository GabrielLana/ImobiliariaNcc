using ImobiliariaNcc.Application.Interfaces.Repositories;
using ImobiliariaNcc.Domain.Models;
using ImobiliariaNcc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ImobiliariaNcc.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    protected readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(T model)
    {
        _context.Add(model);
    }

    public void Update(T model)
    {
        model.AtualizarDataAlteracao();
        _context.Update(model);
    }

    public void Delete(T model)
    {
        _context.Remove(model);
    }

    public async Task<T?> Get(int id, CancellationToken cancellationToken)
        => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        => await _context.Set<T>().ToListAsync(cancellationToken);
}