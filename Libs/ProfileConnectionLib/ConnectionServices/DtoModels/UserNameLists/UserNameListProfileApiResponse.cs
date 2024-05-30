namespace ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;

public record UserNameListProfileApiResponse
{
    public required UsersList UsersList { get; set; }
}

public record UsersList
{
    public required Guid UserId { get; init; }
    
    public required string Name { get; init; }
}
