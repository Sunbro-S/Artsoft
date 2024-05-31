using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Services.Services.Saga;

public class SagaStateMap: SagaClassMap<SagaState>
{
    protected void Configure(EntityTypeBuilder<SagaState> entity, ModelBuilder model)
    {
        base.Configure(entity, model);
        entity.Property(x => x.CurrentState);
    }
}