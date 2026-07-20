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
}