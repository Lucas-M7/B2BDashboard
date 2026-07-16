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
}