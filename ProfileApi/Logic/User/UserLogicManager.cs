using Dal.User.Interfaces;
using Dal.User.Model;
using Logic.User.Interfaces;
using Logic.User.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Logic.User;

public class UserLogicManager : IUserLogicManager
{
    private readonly IUserRepository _userRepository;

    public UserLogicManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CreateUserAsync(UserModel user)
    {
        return await _userRepository.CreateUserAsync(new UserDal
        {
            Email = user.Email,
            Username = user.Username,
            PasswordHash = user.PasswordHash
        });
    }

    public async Task<UserDal> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
}