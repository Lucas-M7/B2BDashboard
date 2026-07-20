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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid companyId, Guid id, CancellationToken ct)
    {
        await saleService.DeleteAsync(id, companyId, ct);
        return NoContent();
    }
}