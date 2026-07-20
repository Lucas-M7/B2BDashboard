using B2BDashboard.Application.DTOs.Clients;

namespace B2BDashboard.Application.Interfaces;

public interface IClientService
{
    Task<ClientResponse> CreateAsync(CreateClientRequest request, Guid companyId, CancellationToken ct = default);
    Task<ClientResponse> UpdateAsync(Guid id, Guid companyId, UpdateClientRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, Guid companyId, CancellationToken ct = default);
}