using Microsoft.EntityFrameworkCore;
using PetPartner.Domain.Entities;
using PetPartner.Domain.Security.Tokens;
using PetPartner.Domain.Services;
using PetPartner.Domain.Services.LoggedUser;
using PetPartner.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PetPartner.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly PetPartnerDbContext _context;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(PetPartnerDbContext context, ITokenProvider tokenProvider)
    {
        _context = context;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenhandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenhandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _context
            .Users
            .AsNoTracking()
            .FirstAsync(u => u.Active && u.UserIdentifier == userIdentifier);
    }
}
