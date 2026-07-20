using B2BDashboard.Application.DTOs.Companies;

namespace B2BDashboard.Application.Interfaces;

public interface ICompanyService
{
    Task<CompanyResponse> CreateAsync(CreateCompanyRequest request, CancellationToken ct = default);
    Task<CompanyResponse> UpdateAsync(Guid id, UpdateCompanyRequest request, CancellationToken ct = default);
    Task DeactivateAsync(Guid id, CancellationToken ct = default);
}