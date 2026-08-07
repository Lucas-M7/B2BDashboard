using B2BDashboard.Api.Extensions;
using B2BDashboard.Application.Common;
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
    [HttpGet]
    public async Task<ActionResult<PagedResult<SaleResponse>>> GetPaged(
        [FromQuery] PaginationQuery query,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken ct)
    {
        var result = await saleService.GetPagedAsync(User.GetCompanyId(), query, from, to, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SaleResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await saleService.GetByIdAsync(id, User.GetCompanyId(), ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<SaleResponse>> Create(
        [FromBody] CreateSaleRequest request, CancellationToken ct)
    {
        var result = await saleService.CreateAsync(request, User.GetCompanyId(), ct);
        return Created($"api/sales/{result.Id}", result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await saleService.DeleteAsync(id, User.GetCompanyId(), ct);
        return NoContent();
    }
}