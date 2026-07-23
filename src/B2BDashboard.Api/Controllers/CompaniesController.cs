using B2BDashboard.Api.Extensions;
using B2BDashboard.Application.DTOs.Companies;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/companies")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpPut("me")]
    public async Task<ActionResult<CompanyResponse>> UpdateOwnCompany(
        [FromBody] UpdateCompanyRequest request, CancellationToken ct)
    {
        var result = await companyService.UpdateAsync(User.GetCompanyId(), request, ct);
        return Ok(result);
    }

    [HttpDelete("me")]
    public async Task<IActionResult> DeactivateOwnCompany(CancellationToken ct)
    {
        await companyService.DeactivateAsync(User.GetCompanyId(), ct);
        return NoContent();
    }
}