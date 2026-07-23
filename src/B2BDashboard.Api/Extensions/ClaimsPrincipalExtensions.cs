using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace B2BDashboard.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetCompanyId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue("companyId")
            ?? throw new InvalidOperationException("Claim 'companyId' não encontrada no token.");

        return Guid.Parse(value);
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new InvalidOperationException("Claim 'sub' não encontrada no token.");

        return Guid.Parse(value);
    }
}