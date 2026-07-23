using B2BDashboard.Api.Extensions;
using B2BDashboard.Application.DTOs.Sales;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/sales")]
public class SalesController(ISaleService saleService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SaleResponse>> Create(
        [FromBody] CreateSaleRequest request,
        CancellationToken ct)
    {
        var result = await saleService.CreateAsync(request, User.GetCompanyId(), ct);
        return Created($"api/companies/sales/{result.Id}", request);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await saleService.DeleteAsync(id, User.GetCompanyId(), ct);
        return NoContent();
    }
}