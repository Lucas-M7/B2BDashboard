using B2BDashboard.Application.Interfaces;
using B2BDashboard.Domain.Entities;
using B2BDashboard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Repositories;

public class UserRepository(AppDbcontext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken ct = default) =>
        await context.Users.AddAsync(user, ct);

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant(), ct);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
}