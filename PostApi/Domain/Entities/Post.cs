using Domain.Interfaces;
using ExampleCore.Dal.Base;

namespace Domain.Entities;

public record Post : BaseEntityDal<Guid>
{
    public required Guid UserId { get; init; }
    public required string Title { get; init; }
    public required DateTime PublicDate { get; init; }
    public required string Content { get; init; }
    public int LikeCount { get; set; }
    public CreatedPostUserName UserName { get; set; }
    
    public async Task<Guid> SaveAsync(IStorePost storePost, ICheckUser chekUser)
    {
        await chekUser.CheckUserExistAsync(UserId);

        var res = await storePost.AddPost(this);
        return res;
    }
}
public class CreatedPostUserName
{
    public required string Name { get; init; }
}