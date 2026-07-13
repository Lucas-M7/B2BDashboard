namespace B2BDashboard.Application.DTOs.Companies;

public record CreateCompanyRequest(string Name, string Cnpj);
public record CompanyResponse(Guid Id, string Name, string Cnpj, DateTime CreatedAt);