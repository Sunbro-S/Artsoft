using Domain.Entities;
using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using ProfileConnectionLib.ConnectionServices.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class CreatePost : ICreatePost
{
    private readonly IStorePost _storePost;
    private readonly ICheckUser _checkUser;
    private readonly IProfileConnectionServcie _profileConnectionServcie;
    public CreatePost(IStorePost storePost, ICheckUser checkUser, IProfileConnectionServcie profileConnectionServcie)
    {
        _storePost = storePost;
        _checkUser = checkUser;
        _profileConnectionServcie = profileConnectionServcie;
    }
    
    public async Task<Post[]> GetPostListAsync()
    {
        var postList = await _storePost.GetAllAsync();
        var userIdList = postList.Select(value => value.UserId).ToArray();
        var userNameList = await _profileConnectionServcie.GetUserNameListAsync(new UserNameListProfileApiRequest
        {
            UserIdList = userIdList
        });

        var userNameDict = userNameList.ToDictionary(value => value.UsersList.UserId, value => value.UsersList.Name);

        foreach (var post in postList)
        {
            if (userNameDict.TryGetValue(post.UserId, out var userName))
            {
                post.UserName = new CreatedPostUserName
                {
                    Name = userName
                };
            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }
        return postList;
    }

    public async Task<Guid> CreatePostAsync(Post post)
    {
        var create = await post.SaveAsync(_storePost, _checkUser);
        return create;
    }

    public async Task<string> DelPostAsync(Guid id)
    {
        var postDeleting = await _storePost.DeletePostAsync(id);
        return postDeleting;
    }
}