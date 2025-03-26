using PetPartner.Communication.Requests.Pet;
using PetPartner.Communication.Responses.Pet;

namespace PetPartner.Application.UseCases.Pet;

public interface IRegisterPetUseCase
{
    Task<ResponseRegisteredPetJson> Execute(RequestPetJson request);
}
