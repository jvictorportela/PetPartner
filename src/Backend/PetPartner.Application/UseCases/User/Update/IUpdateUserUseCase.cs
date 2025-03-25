using PetPartner.Communication.Requests;

namespace PetPartner.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
