using AutoMapper;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;

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
            .ForMember(dest => dest.Address, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseRegisteredUserJson>();
    }
}
