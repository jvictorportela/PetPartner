using PetPartner.Domain.Repositories.Pet;

namespace PetPartner.Infrastructure.DataAccess.Repositories;

public class PetRepository : IPetWriteOnlyRepositoy
{
    private readonly PetPartnerDbContext _context;

    public PetRepository(PetPartnerDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Pet pet) => await _context.Pets.AddAsync(pet);
}
