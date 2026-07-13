using B2BDashboard.Application.DTOs.Sales;
using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;

namespace B2BDashboard.Application.Services;

public class SaleService(IClientRepository clientRepository, ISaleRepository saleRepository,
    IUnitOfWork unitOfWork) : ISaleService
{
    public async Task<SaleResponse> CreateAsync(CreateSaleRequest request, Guid companyId, CancellationToken ct = default)
    {
        var client = await clientRepository.GetByIdAsync(request.ClientId, ct);

        if (client is null || client.CompanyId != companyId)
            throw new NotFoundException("Cliente não encontrado.");

        var sale = Sale.Create(request.Amount, request.Description, client.Id, companyId);

        await saleRepository.AddAsync(sale, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new SaleResponse(sale.Id, sale.Amount, sale.Description, sale.SaleDate, sale.ClientId);
    }
}