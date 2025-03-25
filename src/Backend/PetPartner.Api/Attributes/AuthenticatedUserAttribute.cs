using Microsoft.AspNetCore.Mvc;
using PetPartner.Api.Filters;

namespace PetPartner.Api.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
        
    }
}
