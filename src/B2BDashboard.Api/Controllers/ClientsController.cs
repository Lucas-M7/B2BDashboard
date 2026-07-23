using B2BDashboard.Api.Extensions;
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
    [HttpPost]
    public async Task<ActionResult<ClientResponse>> Create(
        [FromBody] CreateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.CreateAsync(request, User.GetCompanyId(), ct);
        return Created($"api/clients/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientResponse>> Update(
        Guid id, [FromBody] UpdateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.UpdateAsync(id, User.GetCompanyId(), request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await clientService.DeleteAsync(id, User.GetCompanyId(), ct);
        return NoContent();
    }
}