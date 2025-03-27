using Microsoft.EntityFrameworkCore;
using PetPartner.Domain.Repositories.Pet;

namespace PetPartner.Infrastructure.DataAccess.Repositories;

public class PetRepository : IPetWriteOnlyRepositoy, IPetUpdateOnlyRepository
{
    private readonly PetPartnerDbContext _context;

    public PetRepository(PetPartnerDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.Pet pet) => await _context.Pets.AddAsync(pet);

    public async Task<Domain.Entities.Pet> GetById(long id)
    {
        return await _context
            .Pets
            .FirstAsync(pet => pet.Id == id);
    }

    public void Update(Domain.Entities.Pet pet) => _context.Pets.Update(pet);
}
