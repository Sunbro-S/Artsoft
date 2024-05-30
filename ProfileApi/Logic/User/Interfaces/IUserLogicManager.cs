using Dal.User.Model;
using Logic.User.Model;

namespace Logic.User.Interfaces;

public interface IUserLogicManager
{
    
    Task<UserDal> GetUserByIdAsync(Guid id);
    Task<bool> CreateUserAsync(UserModel user);
}