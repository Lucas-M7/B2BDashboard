using System.ComponentModel.DataAnnotations;

namespace B2BDashboard.Application.DTOs.Companies;

public record CreateCompanyRequest(
    [Required, MaxLength(200)] string Name, 
    [Required, MaxLength(20)] string Cnpj);
public record UpdateCompanyRequest([Required, MaxLength(200)] string Name);
public record CompanyResponse(Guid Id, string Name, string Cnpj, DateTime CreatedAt);