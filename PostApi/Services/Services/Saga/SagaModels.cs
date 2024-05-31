namespace Services.Services.Saga;

public record CreatePostSagaRequest(Guid postId, Guid userId)
{
    public CreatePostSagaRequest() : this(default, default) { }
}

public record CreatePostSagaResponse(Guid postId, Guid userId)
{
    public CreatePostSagaResponse() : this(default, default) { }
}

public record CreatePostSagaCompleted(Guid postId, Guid userId)
{
    public CreatePostSagaCompleted() : this(default, default) { }
}

public record CreatePostSagaError(Guid postId, Guid userId, string Reason)
{
    public CreatePostSagaError() : this(default, default, string.Empty) { }
}