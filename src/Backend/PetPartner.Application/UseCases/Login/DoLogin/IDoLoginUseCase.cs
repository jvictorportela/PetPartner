using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;

namespace PetPartner.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
