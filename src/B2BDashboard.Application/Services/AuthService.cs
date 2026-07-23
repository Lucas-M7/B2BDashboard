using B2BDashboard.Application.Common;
using B2BDashboard.Application.DTOs.Auth;
using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Domain.Enums;
using Microsoft.Extensions.Options;

namespace B2BDashboard.Application.Services;

public class AuthService(
    ICompanyRepository companyRepository,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IUnitOfWork unitOfWork,
    IOptions<JwtSettings> jwtOptions) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, ct);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Email ou senha inválidos.");

        return await IssueTokensAsync(user, ct);
    }

    public async Task<AuthResponse> RefreshAsync(RefreshRequest request, CancellationToken ct = default)
    {
        var storedToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, ct);

        if (storedToken is null || !storedToken.IsActive)
            throw new UnauthorizedException("Refresh token inválido ou expirado.");

        storedToken.Revoke(); // rotação, este token não pode ser usado de novo

        var user = await userRepository.GetByIdAsync(storedToken.UserId, ct)
            ?? throw new UnauthorizedException("Usuário não encontrado.");

        return await IssueTokensAsync(user, ct);
    }

    public async Task<AuthResponse> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken ct = default)
    {
        if (await companyRepository.GetByCnpjAsync(request.Cnpj, ct) is not null)
            throw new ConflictException($"Já existe uma empresa cadastrada com o CNPJ {request.Cnpj}.");

        if (await userRepository.GetByEmailAsync(request.AdminEmail, ct) is not null)
            throw new ConflictException($"Já existe uma usuário cadastado com o email {request.AdminEmail}.");

        var company = Company.Create(request.CompanyName, request.Cnpj);
        var passwordHash = passwordHasher.Hash(request.Password);
        var adminUser = User.Create(request.AdminName, request.AdminEmail, passwordHash, UserRole.Admin, company.Id);

        await companyRepository.AddAsync(company, ct);
        await userRepository.AddAsync(adminUser, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return await IssueTokensAsync(adminUser, ct);
    }

    private async Task<AuthResponse> IssueTokensAsync(User user, CancellationToken ct)
    {
        var (accessToken, accessTokenExpiresAt) = jwtTokenGenerator.GenerateAccessToken(user);
        var refreshTokenValue = jwtTokenGenerator.GenerateRefreshToken();

        var refreshToken = RefreshToken.Create(
            refreshTokenValue,
            DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays), 
            user.Id
        );

        await refreshTokenRepository.AddAsync(refreshToken, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new AuthResponse(accessToken, refreshTokenValue, accessTokenExpiresAt);
    }
}