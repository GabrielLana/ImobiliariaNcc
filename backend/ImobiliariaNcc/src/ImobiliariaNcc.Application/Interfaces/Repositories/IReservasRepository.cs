using ImobiliariaNcc.Domain.Models;

namespace ImobiliariaNcc.Application.Interfaces.Repositories;

public interface IReservasRepository : IBaseRepository<ReservaModel>
{
    Task<bool> ClientePossuiReservaAtiva(int idCliente, CancellationToken ct);
    Task<bool> ApartamentoPossuiReservaAtiva(int idApartamento, CancellationToken ct);
    Task<ReservaModel?> GetReservaAtiva(int idCliente, int idApartamento, CancellationToken ct);
}
