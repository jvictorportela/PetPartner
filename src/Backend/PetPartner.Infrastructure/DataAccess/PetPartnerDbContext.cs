using Microsoft.EntityFrameworkCore;

namespace PetPartner.Infrastructure.DataAccess;

public class PetPartnerDbContext : DbContext
{
    public PetPartnerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Domain.Entities.Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetPartnerDbContext).Assembly);
    }
}
