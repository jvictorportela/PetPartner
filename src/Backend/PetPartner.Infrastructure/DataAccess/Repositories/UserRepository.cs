using Microsoft.EntityFrameworkCore;
using PetPartner.Domain.Repositories.User;

namespace PetPartner.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly PetPartnerDbContext _context;

    public UserRepository(PetPartnerDbContext context)
    {
        _context = context;
    }

    public async Task Add(Domain.Entities.User user) => await _context.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _context.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<Domain.Entities.User?> GetByEmailAndPassword(string email, string password)
    {
        return await _context
            .Users
            .AsNoTracking() //Sempre que houver certeza de ações que nao podem ser atualizadas(read-only), o AsNoTracking deve ser utilizado para melhorar a performance.
            .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
    }
}
