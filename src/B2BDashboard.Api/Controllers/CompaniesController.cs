using B2BDashboard.Application.DTOs.Companies;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CompanyResponse>> Create(
        [FromBody] CreateCompanyRequest request,
        CancellationToken ct)
    {
        var result = await companyService.CreateAsync(request, ct);
        return Created($"api/companies/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CompanyResponse>> Update(
        Guid id, [FromBody] UpdateCompanyRequest request, CancellationToken ct)
    {
        var result = await companyService.UpdateAsync(id, request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Deactivate(Guid id, CancellationToken ct)
    {
        await companyService.DeactivateAsync(id, ct);
        return NoContent();
    }
}