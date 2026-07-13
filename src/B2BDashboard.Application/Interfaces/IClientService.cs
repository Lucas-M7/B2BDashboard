using B2BDashboard.Application.DTOs.Clients;

namespace B2BDashboard.Application.Interfaces;

public interface IClientService
{
    Task<ClientResponse> CreateAsync(CreateClientRequest request, Guid companyId, CancellationToken ct = default);
}