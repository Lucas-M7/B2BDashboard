using B2BDashboard.Domain.Common;

namespace B2BDashboard.Domain.Entities;

public class Sale : BaseEntity
{
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; } = null!;

    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;

    protected Sale() { }

    public static Sale Create(decimal amount, string description, Guid clientId, Guid companyId)
    {
        if (amount <= 0)
            throw new ArgumentException("O valor da venda deve ser maior que zero.", nameof(amount));

        return new Sale
        {
            Amount = amount,
            Description = description,
            SaleDate = DateTime.UtcNow,
            ClientId = clientId,
            CompanyId = companyId
        };
    }
}