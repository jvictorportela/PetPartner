using PetPartner.Communication.Responses;

namespace PetPartner.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
