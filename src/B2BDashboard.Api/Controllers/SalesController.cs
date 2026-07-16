using B2BDashboard.Application.DTOs.Sales;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Route("api/companies/{companyId:guid}/sales")]
public class SalesController(ISaleService saleService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SaleResponse>> Create(
        Guid companyId,
        [FromBody] CreateSaleRequest request,
        CancellationToken ct)
    {
        var result = await saleService.CreateAsync(request, companyId, ct);
        return Created($"api/companies/{companyId}/sales/{result.Id}", request);
    }
}