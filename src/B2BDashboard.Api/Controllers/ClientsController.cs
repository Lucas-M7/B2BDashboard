using B2BDashboard.Application.DTOs.Clients;
using B2BDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B2BDashboard.Api.Controllers;

[ApiController]
[Route("api/companies/{companyId:guid}/clients")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ClientResponse>> Create(
        Guid companyId,
        [FromBody] CreateClientRequest request,
        CancellationToken ct)
    {
        var result = await clientService.CreateAsync(request, companyId, ct);
        return Created($"api/companies/{companyId}/clients/{result.Id}", result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientResponse>> Update(
        Guid companyId, Guid id, 
        [FromBody] UpdateClientRequest request, 
        CancellationToken ct)
    {
        var result = await clientService.UpdateAsync(id, companyId, request, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid companyId, Guid id, CancellationToken ct)
    {
        await clientService.DeleteAsync(id, companyId, ct);
        return NoContent();
    }
}