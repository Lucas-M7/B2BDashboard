using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Company?> GetByCnpjAsync(string cnpj, CancellationToken ct = default);
    Task AddAsync(Company company, CancellationToken ct = default);
}