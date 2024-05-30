using Dal.User.Interfaces;
using Dal.User.Model;
using Microsoft.EntityFrameworkCore;

namespace Dal.User;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDal> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user is not null)
        {
            return user;
        }

        throw new Exception("Такого пользователя не существует.");
    }

    public async Task<bool> CreateUserAsync(UserDal user)
    {
        if (user.Id == Guid.Empty)
        {
            user.Id = Guid.NewGuid();
        }

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var dbSet = _context.Users;
                await dbSet.AddAsync(user);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}