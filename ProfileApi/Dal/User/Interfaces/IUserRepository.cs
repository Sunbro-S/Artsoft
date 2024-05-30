using Dal.User.Model;

namespace Dal.User.Interfaces;

public interface IUserRepository
{
    
    Task<UserDal> GetUserByIdAsync(Guid id);
    Task<bool> CreateUserAsync(UserDal user);
}