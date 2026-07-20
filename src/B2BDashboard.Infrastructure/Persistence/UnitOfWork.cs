using B2BDashboard.Application.Exceptions;
using B2BDashboard.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace B2BDashboard.Infrastructure.Persistence;

public class UnitOfWork(AppDbcontext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch(DbUpdateException ex) when (ex.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.ForeignKeyViolation
        })
        {
            throw new ConflictException(
                "Não é possível excluir este registro: existem dados vinculados a ele (ex: vendas associadas ao cliente).");
        }
    }
}