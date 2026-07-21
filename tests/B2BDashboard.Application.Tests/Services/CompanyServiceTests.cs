using B2BDashboard.Application.DTOs.Companies;
using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Application.Services;
using B2BDashboard.Domain.Entities;
using FluentAssertions;
using Moq;

namespace B2BDashboard.Application.Tests.Services;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _companyRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly CompanyService _sut; // sut = "system under test"

    public CompanyServiceTests()
    {
        _sut = new CompanyService(_companyRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateAsync_WithNewCnpj_ShouldCreateCompanyAndSave()
    {
        // Arrange
        var request = new CreateCompanyRequest("ACme Ltda", "12345678000199");

        _companyRepositoryMock
            .Setup(r => r.GetByCnpjAsync(request.Cnpj, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company?)null); // simula "não existe empresa com esse CNPJ ainda."

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        result.Name.Should().Be(request.Name);
        result.Cnpj.Should().Be(request.Cnpj);

        _companyRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(
            u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateASync_WithExistingCnpj_ShouldThrowConflictAndNotSave()
    {
        // Arrange
        var request = new CreateCompanyRequest("Acme Ltda", "12345678000199");
        var existingCompany = Company.Create("Empresa já cadastrada", request.Cnpj);

        _companyRepositoryMock
            .Setup(r => r.GetByCnpjAsync(request.Cnpj, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCompany); // simula "já existe"

        // Act
        var act = () => _sut.CreateAsync(request);

        // Assert
        await act.Should().ThrowAsync<ConflictException>();

        _companyRepositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}