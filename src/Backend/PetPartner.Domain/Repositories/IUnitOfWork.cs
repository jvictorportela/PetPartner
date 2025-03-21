namespace PetPartner.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}
