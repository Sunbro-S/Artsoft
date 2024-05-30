using CoreLib.HttpServiceV2.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using ProfileConnectionLib.ConnectionServices.RabbitMQConnectionServer;
using ProfileConnectionLib.ConnectionServices.RabbitMQConnectionServer.Service;
using RabbitMQ.Client;

namespace ProfileConnectionLib;

public static class ProfileLibStartUp
{
    public static IServiceCollection TryAddProfileLib(this IServiceCollection services)
    {
        services.TryAddScoped<IProfileConnectionServcie, ProfileConnectionService>();
        return services;
    }
    public static IServiceCollection AddRabbitLogic(this IServiceCollection services)
    {
        services.TryAddSingleton<IPooledObjectPolicy<IConnection>>(_ =>
        {
            return new ConnectionPool("localhost");
        });
        
        services.TryAddSingleton<ObjectPool<IConnection>>(serviceProvider =>
        {
            var policy = serviceProvider.GetRequiredService<IPooledObjectPolicy<IConnection>>();
            return new DefaultObjectPool<IConnection>(policy);
        });
        services.TryAddKeyedTransient<IHttpRequestService, RequestServer>("rabbitmq");
        return services;
    }
}