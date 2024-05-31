using MassTransit;

namespace Services.Services.Saga;

public class SagaStateMachine: MassTransitStateMachine<SagaState>
{
    public SagaStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreatePostRequested, x => x.CorrelateById(context => context.Message.userId));
        Event(() => UserNotFound, x => x.CorrelateById(context => context.Message.userId));
        Event(() => PostCreated, x => x.CorrelateById(context => context.Message.userId));

        Initially(
            When(CreatePostRequested)
                .Then(context =>
                {
                    context.Instance.UserId = context.Data.userId;
                    context.Instance.PostId = context.Data.postId;
                })
                .Publish(ctx => new CreatePostSagaRequest(ctx.Instance.PostId, ctx.Instance.UserId))
                .TransitionTo(CreatingTask)
        );

        During(CreatingTask,
            When(UserNotFound)
                .Then(context => throw new Exception("Пользователь не найден"))
                .TransitionTo(Failed)
        );

        During(CreatingTask,
            When(PostCreated)
                .TransitionTo(Success)
        );

        SetCompletedWhenFinalized();
    }

    public State CreatingTask { get; private set; }
    public State Success { get; private set; }
    public State Failed { get; private set; }

    public Event<CreatePostSagaRequest> CreatePostRequested { get; private set; }
    public Event<CreatePostSagaResponse> PostCreated { get; private set; }
    public Event<CreatePostSagaError> UserNotFound { get; private set; }
}