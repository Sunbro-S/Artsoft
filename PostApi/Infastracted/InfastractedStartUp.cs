using Domain.Interfaces;
using Infastracted.Connections;
using Infastracted.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;

namespace Infastracted;

public static class InfastractedStartUp
{
    public static IServiceCollection TryAddInfastracted(this IServiceCollection services)
    {
        services.TryAddScoped<IStorePost, PostRepository>();
        services.TryAddScoped<ILikePost, LikeRepository>();
        services.TryAddScoped<ICheckUser, CheckUser>();

        return services;
    }
    
    public static IServiceCollection TryAddApplicationContext(this IServiceCollection serviceCollection, IConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        return serviceCollection;
    }
}