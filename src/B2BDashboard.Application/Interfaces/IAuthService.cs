using B2BDashboard.Application.DTOs.Auth;

namespace B2BDashboard.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterCompanyAsync(RegisterCompanyRequest request, CancellationToken ct = default);
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<AuthResponse> RefreshAsync(RefreshRequest request, CancellationToken ct = default);
}