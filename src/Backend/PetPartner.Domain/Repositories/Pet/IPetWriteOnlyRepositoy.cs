namespace PetPartner.Domain.Repositories.Pet;

public interface IPetWriteOnlyRepositoy
{
    Task Add(Entities.Pet pet);
}
