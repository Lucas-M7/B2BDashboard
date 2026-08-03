using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Repositories;

public class SaleRepository(AppDbcontext context) : ISaleRepository
{
    public async Task AddAsync(Sale sale, CancellationToken ct = default) =>
        await context.Sales.AddAsync(sale, ct);

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Sales.FirstOrDefaultAsync(c => c.Id == id, ct);

    public void Remove(Sale sale) => context.Sales.Remove(sale);

    public Task<Sale?> GetByIdWithClientAsync(Guid id, CancellationToken ct = default) =>
    context.Sales.Include(s => s.Client).FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<(IReadOnlyList<Sale> Items, int TotalCount)> GetPagedByCompanyIdAsync(
        Guid companyId, int page, int pageSize, DateTime? from, DateTime? to, CancellationToken ct = default)
    {
        var query = context.Sales
            .Include(s => s.Client)
            .Where(s => s.CompanyId == companyId);

        if (from.HasValue)
            query = query.Where(s => s.SaleDate >= from.Value);

        if (to.HasValue)
            query = query.Where(s => s.SaleDate <= to.Value);

        query = query.OrderByDescending(s => s.SaleDate);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}