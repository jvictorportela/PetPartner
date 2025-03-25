using PetPartner.Communication.Requests;

namespace PetPartner.Application.UseCases.User.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
