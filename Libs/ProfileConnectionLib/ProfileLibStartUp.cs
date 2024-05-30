using CoreLib.HttpServiceV2.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace ProfileConnectionLib;

public static class ProfileLibStartUp
{
    public static IServiceCollection TryAddProfileLib(this IServiceCollection services)
    {
        services.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        return services;
    }
}