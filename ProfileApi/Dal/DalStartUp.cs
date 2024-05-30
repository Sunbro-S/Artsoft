using Dal.User;
using Dal.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dal;

public static class DalStartUp
{
    public static IServiceCollection AddDal(this IServiceCollection service)
    {
        service.TryAddScoped<IUserRepository, UserRepository>();
        return service;
    }
}