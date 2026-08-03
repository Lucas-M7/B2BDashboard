using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Interfaces;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Sale sale, CancellationToken ct = default);
    Task<Sale?> GetByIdWithClientAsync(Guid id, CancellationToken ct = default);
    Task<(IReadOnlyList<Sale> Items, int TotalCount)> GetPagedByCompanyIdAsync(
        Guid companyId, int page, int pageSize, DateTime? from, DateTime? to,
        CancellationToken ct = default);
    void Remove(Sale sale);
}