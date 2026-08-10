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
    /// <summary>
    /// Busca em uma página com clientes cadastrados.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="ct"></param>
    /// <response code="200">Busca realizada com sucesso.</response>
    /// <response code="401">Token ausente ou inválido.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResult<ClientResponse>>> GetPaged(
        [FromQuery] PaginationQuery query, CancellationToken ct)
    {
        var result = await clientService.GetPagedAsync(User.GetCompanyId(), query, ct);
        return Ok(result);
    }

    /// <summary>
    /// Pega um cliente através do seu ID.
    /// </summary>
    /// <param name="id">Id do cliente.</param>
    /// <param name="ct"></param>
    /// <response code="200">Busca realizada com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos (formato incorreto).</response>
    /// <response code="401">Token ausente ou inválido.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClientResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await clientService.GetByIdAsync(id, User.GetCompanyId(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Cria um novo cliente para a empresa do usuário autenticado.
    /// </summary>
    /// <param name="request">Dados do cliente a ser cliado.</param>
    /// <param name="ct"></param>
    /// <response code="201">Cliente criado com sucesso.</response>
    /// <response code="400">Dados de entrada inválidos (formato incorreto).</response>
    /// <response code="401">Token ausente ou inválido.</response>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ClientResponse>> Create(
        [FromBody] CreateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.CreateAsync(request, User.GetCompanyId(), ct);
        return Created($"api/clients/{result.Id}", result);
    }

    /// <summary>
    /// Atualiza um cliente cadastrado.
    /// </summary>
    /// <param name="id">ID do cliente a ser atualizado.</param>
    /// <param name="request">Dados do cliente a serem atualizados.</param>
    /// <param name="ct"></param>
    /// <response code="400">Dados de entrada inválidos (formato incorreto).</response>
    /// <response code="401">Token ausente ou inválido.</response>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<ClientResponse>> Update(
        Guid id, [FromBody] UpdateClientRequest request, CancellationToken ct)
    {
        var result = await clientService.UpdateAsync(id, User.GetCompanyId(), request, ct);
        return Ok(result);
    }

    /// <summary>
    /// Deleta um cliente.
    /// </summary>
    /// <param name="id">ID do cliente a ser deletado.</param>
    /// <param name="ct"></param>
    /// <response code="400">Dados de entrada inválidos (formato incorreto).</response>
    /// <response code="401">Token ausente ou inválido.</response>
    /// <response code="204">Deleção concluída com sucesso.</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await clientService.DeleteAsync(id, User.GetCompanyId(), ct);
        return NoContent();
    }
}