namespace PetPartner.Domain.Repositories.Pet;

public interface IPetUpdateOnlyRepository
{
    Task<Entities.Pet> GetById(long id);
    void Update(Entities.Pet pet);
}
