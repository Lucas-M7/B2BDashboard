using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Repositories;

public class ClientRepository(AppDbcontext context) : IClientRepository
{
    public async Task AddAsync(Client client, CancellationToken ct = default) =>
        await context.Clients.AddAsync(client, ct);

    public async Task<IReadOnlyList<Client>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default) =>
        await context.Clients.Where(c => c.CompanyId == companyId).ToListAsync(ct);

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Clients.FirstOrDefaultAsync(c => c.Id == id, ct);
}