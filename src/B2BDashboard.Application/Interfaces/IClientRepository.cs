using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Client>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default);
    Task AddAsync(Client client, CancellationToken ct = default);
    void Remove(Client client);
}