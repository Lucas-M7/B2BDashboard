using B2BDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace B2BDashboard.Infrastructure.Persistence;

public class AppDbcontext : DbContext
{
    public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Sale> Sales => Set<Sale>();

    protected void OModelCreating(ModelBuilder modelBuilder)
    {
        // Escaneia o assembly procurando por classes que implementam o IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbcontext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}