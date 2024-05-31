using MassTransit;
using MassTransit.Saga;
using Microsoft.EntityFrameworkCore;

namespace Services.Services.Saga;

public class SagaDbContext<TSaga>(DbContextOptions options) : ISagaRepositoryContextFactory<TSaga>
    where TSaga : class, ISaga
{
    private readonly DbContextOptions _options = options;

    public void Probe(ProbeContext context)
    {
        throw new NotImplementedException();
    }

    public Task Send<T>(ConsumeContext<T> context, IPipe<SagaRepositoryContext<TSaga, T>> next) where T : class
    {
        throw new NotImplementedException();
    }

    public Task SendQuery<T>(ConsumeContext<T> context, ISagaQuery<TSaga> query, IPipe<SagaRepositoryQueryContext<TSaga, T>> next) where T : class
    {
        throw new NotImplementedException();
    }
}