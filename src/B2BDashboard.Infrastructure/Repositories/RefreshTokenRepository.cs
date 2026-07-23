using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbcontext context) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken refreshToken, CancellationToken ct = default) =>
        await context.RefreshTokens.AddAsync(refreshToken, ct);

    public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default) =>
        context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, ct);
}