using B2BDashboard.Application.Interfaces;

namespace B2BDashboard.Infrastructure.Persistence;

public class UnitOfWork(AppDbcontext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        context.SaveChangesAsync(cancellationToken);
}