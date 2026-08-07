using B2BDashboard.Api.Extensions;
using B2BDashboard.Application.Common;
using B2BDashboard.Application.DTOs.Clients;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/clients")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ClientResponse>>> GetPaged(
        [FromQuery] PaginationQuery query, CancellationToken ct)
    {
        var result = await clientService.GetPagedAsync(User.GetCompanyId(), query, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await clientService.GetByIdAsync(id, User.GetCompanyId(), ct);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<ClientResponse>> Create(
        [FromBody] CreateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.CreateAsync(request, User.GetCompanyId(), ct);
        return Created($"api/clients/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<ClientResponse>> Update(
        Guid id, [FromBody] UpdateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.UpdateAsync(id, User.GetCompanyId(), request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await clientService.DeleteAsync(id, User.GetCompanyId(), ct);
        return NoContent();
    }
}