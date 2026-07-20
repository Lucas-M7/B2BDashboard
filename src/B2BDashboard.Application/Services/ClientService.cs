using B2BDashboard.Application.DTOs.Clients;
using B2BDashboard.Application.Exceptions;
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

    public async Task DeleteAsync(Guid id, Guid companyId, CancellationToken ct = default)
    {
        var client = await clientRepository.GetByIdAsync(id, ct);
        if (client is null || client.CompanyId != companyId)
            throw new NotFoundException("Cliente não encontrado.");

        clientRepository.Remove(client);
        await unitOfWork.SaveChangesAsync(ct);
    }

    public Task<ClientResponse> UpdateAsync(Guid id, Guid companyId, UpdateClientRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}