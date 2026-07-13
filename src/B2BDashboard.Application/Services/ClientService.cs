using B2BDashboard.Application.DTOs.Clients;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Services;

public class ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork) : IClientService
{
    public async Task<ClientResponse> CreateAsync(CreateClientRequest request, Guid companyId, CancellationToken ct = default)
    {
        var client = Client.Create(request.Name, request.Document, request.Email, companyId);

        await clientRepository.AddAsync(client, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new ClientResponse(client.Id, client.Name, client.Document, client.Email);
    }
}