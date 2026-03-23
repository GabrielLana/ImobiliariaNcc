using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseModel
{
    void Create(T model);
    void Update(T model);
    void Delete(T model);
    Task<T?> Get(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAll(CancellationToken cancellationToken);
}
