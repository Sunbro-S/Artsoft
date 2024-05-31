using MassTransit;

namespace Services.Services.Saga;

public class SagaState: SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public State CurrentState { get; set; }
}