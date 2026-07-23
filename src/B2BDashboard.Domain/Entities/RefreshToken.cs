using B2BDashboard.Domain.Common;

namespace B2BDashboard.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    protected RefreshToken() { }

    public static RefreshToken Create(string token, DateTime expiresAt, Guid userId) =>
        new() { Token = token, ExpiresAt = expiresAt, UserId = userId };

    public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;

    public void Revoke() => RevokedAt = DateTime.UtcNow;
}