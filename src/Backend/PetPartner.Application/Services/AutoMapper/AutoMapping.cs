using AutoMapper;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Requests.Pet;
using PetPartner.Communication.Requests.Picture;
using PetPartner.Communication.Responses;
using PetPartner.Communication.Responses.Pet;

namespace PetPartner.Application.Services.AutoMapper;

public class AutoMapping :  Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>() //FONTE, DESTINO
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Pets, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore());

        CreateMap<RequestPetJson, Domain.Entities.Pet>()
            .ForMember(dest => dest.Pictures, opt => opt.Ignore()); // Ignorar para nao mapear no momento exato, e sim depois seguindo o fluxo.

        CreateMap<RequestPictureJson, Domain.Entities.Picture>();
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

        CreateMap<Domain.Entities.Pet, ResponseRegisteredPetJson>(); //Configurar Id da resposta com SQUID.
    }
}
