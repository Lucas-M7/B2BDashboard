using System.ComponentModel.DataAnnotations;

namespace B2BDashboard.Application.DTOs.Sales;

public record CreateSaleRequest(
    decimal Amount, 
    [Required] string Description, Guid ClientId);
public record SaleResponse(Guid Id, decimal Amount, string Description, DateTime SaleDate, Guid ClientId);