using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PetPartner.Communication.Responses;
using PetPartner.Domain.Extensions;
using PetPartner.Domain.Repositories.User;
using PetPartner.Domain.Security.Tokens;
using PetPartner.Exceptions.ExceptionBase;

namespace PetPartner.Api.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var useridentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exist = await _repository.ExistActiveUserWithIdentifier(useridentifier);

            if (exist.IsFalse())
                throw new PetPartnerException("No permission for this user.");
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("Token Expired.")
            {
                TokenIsExpired = true
            });
        }
        catch (PetPartnerException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("No permission for this user."));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authentication))
            throw new PetPartnerException("Token not found");

        return authentication["Bearer ".Length..].Trim();
    }
}
