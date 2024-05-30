using Logic.User;
using Logic.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Logic;

public static class LogicStartUp
{
    public static IServiceCollection AddLogic(this IServiceCollection services) 
    {
        services.TryAddScoped<IUserLogicManager, UserLogicManager>();
        return services;
    }
}