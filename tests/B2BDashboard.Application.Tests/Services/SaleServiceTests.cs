using B2BDashboard.Application.DTOs.Sales;
using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Application.Services;
using B2BDashboard.Domain.Entities;
using FluentAssertions;
using Moq;

namespace B2BDashboard.Application.Tests.Services;

public class SaleServiceTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<ISaleRepository> _saleRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly SaleService _sut;

    public SaleServiceTests()
    {
        _sut = new SaleService(_clientRepositoryMock.Object, _saleRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateAsync_WithClientFromAnotherCompany_ShouldThrowNotFound()
    {
        // Arrange
        var requestingComapnyId = Guid.NewGuid();
        var otherCompanyId = Guid.NewGuid();

        var clientFromAnotherCompany = Client.Create("João", "111", "joao@x.com", otherCompanyId);
        var request = new CreateSaleRequest(100m, "Venda suspeita", clientFromAnotherCompany.Id);

        _clientRepositoryMock
            .Setup(r => r.GetByIdAsync(clientFromAnotherCompany.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(clientFromAnotherCompany);

        // Act
        var act = () => _sut.CreateAsync(request, requestingComapnyId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();

        _saleRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}