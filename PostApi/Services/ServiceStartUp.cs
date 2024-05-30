using ExampleCore.HttpLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileConnectionLib;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;
using Services.Services;

namespace Services;

public static class ServiceStartUp
{
    public static IServiceCollection TryAddService(this IServiceCollection services)
    {
        services.TryAddScoped<ICreatePost, CreatePost>();
        services.TryAddScoped<ILikeService, LikeService>();
        services.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        services.AddHttpRequestService();
        services.TryAddProfileLib();
        return services;
    }
}