using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;

namespace PetPartner.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
