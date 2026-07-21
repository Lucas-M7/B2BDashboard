using B2BDashboard.Domain.Entities;
using FluentAssertions;

namespace B2BDashboard.Application.Tests.Domain;

public class SaleTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Create_WithNonPositiveAmount_ShouldThrowArgumentExcpetion(decimal invalidAmount)
    {
        // Act
        var act = () => Sale.Create(invalidAmount, "Venda teste", Guid.NewGuid(), Guid.NewGuid());

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_WithValidData_ShouldCreateSale()
    {
        var clientId = Guid.NewGuid();
        var companyId = Guid.NewGuid();

        var sale = Sale.Create(150.50m, "Consultoria", clientId, companyId);

        sale.Amount.Should().Be(150.50m);
        sale.ClientId.Should().Be(clientId);
        sale.CompanyId.Should().Be(companyId);
    }
}