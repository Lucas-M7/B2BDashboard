using B2BDashboard.Application.DTOs.Sales;

namespace B2BDashboard.Application.Interfaces;

public interface ISaleService
{
    Task<SaleResponse> CreateAsync(CreateSaleRequest request, Guid companyId, CancellationToken ct = default);
}