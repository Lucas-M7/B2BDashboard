using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Interfaces;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Sale sale, CancellationToken ct = default);
    void Remove(Sale sale);
}