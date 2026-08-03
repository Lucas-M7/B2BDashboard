using B2BDashboard.Application.Common;
using B2BDashboard.Application.DTOs.Sales;

namespace B2BDashboard.Application.Interfaces;

public interface ISaleService
{
    Task<SaleResponse> CreateAsync(CreateSaleRequest request, Guid companyId, CancellationToken ct = default);
    Task DeleteAsync(Guid id, Guid companyId, CancellationToken ct = default);
    Task<PagedResult<SaleResponse>> GetPagedAsync(
    Guid companyId, PaginationQuery query, DateTime? from, DateTime? to, CancellationToken ct = default);
    Task<SaleResponse> GetByIdAsync(Guid id, Guid companyId, CancellationToken ct = default);
}