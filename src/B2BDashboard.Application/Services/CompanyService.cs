using B2BDashboard.Application.DTOs.Companies;
using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Services;

public class CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork) : ICompanyService
{
    public async Task<CompanyResponse> CreateAsync(CreateCompanyRequest request, CancellationToken ct = default)
    {
        var existing = await companyRepository.GetByCnpjAsync(request.Cnpj, ct);
        if (existing is not null)
            throw new ConflictException($"Já existe uma empresa cadastrada com o CNPJ {request.Cnpj}.");

        var company = Company.Create(request.Name, request.Cnpj);

        await companyRepository.AddAsync(company, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new CompanyResponse(company.Id, company.Name, company.Cnpj, company.CreatedAt);
    }

    public async Task DeactivateAsync(Guid id, CancellationToken ct = default)
    {
        var company = await companyRepository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Empresa não encontrada.");

        company.Deactivate();
        await unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<CompanyResponse> UpdateAsync(Guid id, UpdateCompanyRequest request, CancellationToken ct = default)
    {
        var company = await companyRepository.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Empresa não encontrada.");

        company.Rename(request.Name);
        await unitOfWork.SaveChangesAsync(ct);

        return new CompanyResponse(company.Id, company.Name, company.Cnpj, company.CreatedAt);
    }
}