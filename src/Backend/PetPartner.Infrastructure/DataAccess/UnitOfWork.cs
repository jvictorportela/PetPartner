using PetPartner.Domain.Repositories;

namespace PetPartner.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly PetPartnerDbContext _context;

    public UnitOfWork(PetPartnerDbContext context)
    {
        _context = context;
    }

    public async Task Commit() => await _context.SaveChangesAsync();
}
