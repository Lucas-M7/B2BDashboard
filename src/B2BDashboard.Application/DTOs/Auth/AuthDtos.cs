using System.ComponentModel.DataAnnotations;

namespace B2BDashboard.Application.DTOs.Auth;

public record RegisterCompanyRequest(
    [Required, MaxLength(200)] string CompanyName,
    [Required, MaxLength(20)] string Cnpj,
    [Required, MaxLength(150)] string AdminName,
    [Required, EmailAddress] string AdminEmail,
    [Required, MinLength(8)] string Password);

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);

public record RefreshRequest([Required] string RefreshToken);

public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);