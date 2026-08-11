using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Application.Services;
using B2BDashboard.Domain.Entities;
using FluentAssertions;
using Moq;

namespace B2BDashboard.Application.Tests.Service;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly ClientService _sut;

    public ClientServiceTests()
    {
        _sut = new ClientService(_clientRepositoryMock.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task GetByIdAsync_WithClientFromAnotherCompany_ShouldThrowNotFound()
    {
        var otherCompanyId = Guid.NewGuid();
        var client = Client.Create("João", "111", "joao@x.com", otherCompanyId);

        _clientRepositoryMock
            .Setup(r => r.GetByIdAsync(client.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        var act = () => _sut.GetByIdAsync(client.Id, Guid.NewGuid(), default);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}