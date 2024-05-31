using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Services.Services.Saga;

public class DbContext : SagaDbContext
{
    public DbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override IEnumerable<ISagaClassMap> Configurations { get; }
}