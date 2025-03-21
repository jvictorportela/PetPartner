using Microsoft.EntityFrameworkCore;
using PetPartner.Domain.Entities;
using PetPartner.Domain.Repositories.User;

namespace PetPartner.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly PetPartnerDbContext _context;

    public UserRepository(PetPartnerDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
}
