using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Repositories;

public class CompanyRepository(AppDbcontext context) : ICompanyRepository
{
    public async Task AddAsync(Company company, CancellationToken ct = default) =>
        await context.Companies.AddAsync(company, ct);

    public async Task<Company?> GetByCnpjAsync(string cnpj, CancellationToken ct = default) =>
        await context.Companies.FirstOrDefaultAsync(c => c.Cnpj == cnpj, ct);

    public async Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.Companies.FirstOrDefaultAsync(c => c.Id == id, ct);
}