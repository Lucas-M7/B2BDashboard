using B2BDashboard.Domain.Common;
using B2BDashboard.Domain.Enums;

namespace B2BDashboard.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; } = null!;

    protected User() { }

    public static User Create(
        string name, 
        string email, 
        string PasswordHash, 
        UserRole role, 
        Guid companyId)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email inválido.", nameof(email));

        return new User
        {
            Name = name,
            Email = email.ToLowerInvariant(),
            PasswordHash = PasswordHash,
            Role = role,
            CompanyId = companyId
        };
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
        MarkAsUpdated();
    }
}