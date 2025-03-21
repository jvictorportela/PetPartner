using AutoMapper;
using PetPartner.Communication.Requests;
using PetPartner.Communication.Responses;
using PetPartner.Domain.Entities;
using System.Drawing;

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
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Address, opt => opt.Ignore());
    }

    private void DomainToResponse()
    {
        CreateMap<User, ResponseRegisteredUserJson>();
    }
}
