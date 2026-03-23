namespace ImobiliariaNcc.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}