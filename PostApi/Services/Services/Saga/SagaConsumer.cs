using MassTransit;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace Services.Services.Saga;

public class SagaConsumer(IProfileConnectionServcie profileConnectionServcie) : IConsumer<CreatePostSagaRequest>
{
    private readonly IProfileConnectionServcie _profileConnectionServcie = profileConnectionServcie;

    public async Task Consume(ConsumeContext<CreatePostSagaRequest> context)
    {
        try
        {
            var user = await _profileConnectionServcie.CheckUserExistAsync(new CheckUserExistProfileApiRequest
            {
                UserId = context.Message.userId
            });

            if (user == null)
            {
                await context.Publish<CreatePostSagaError>(new
                {
                    context.Message.userId,
                    context.Message.postId,
                    Reason = "Пользователь не найден"
                });
            }
            else
            {
                await context.Publish<CreatePostSagaResponse>(new
                {
                    context.Message.userId,
                    context.Message.postId
                });
            }
        }
        catch (Exception ex)
        {
            await context.Publish<CreatePostSagaError>(new
            {
                context.Message.userId,
                context.Message.postId,
                Reason = ex.Message
            });
        }
    }
}