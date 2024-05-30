using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class LikeService : ILikeService
{
    private readonly ILikePost _likePost;

    public LikeService(ILikePost likePost)
    {
        _likePost = likePost;
    }
    
    public async Task<int> GetLikesCountServiceAsync(LikeCounter likeCounter, Post post)
    {
        var res = await _likePost.GetLikesCountAsync(likeCounter, post);
        return res;
    }

    public async Task<int> AddPlusOneLikeServiceAsync(LikeCounter likeCounter)
    {
        var res = await _likePost.AddPlusOneLikeAsync(likeCounter);
        return res;
    }

    public async Task<int> DelMinusOneLikeServiceAsync(LikeCounter likeCounter)
    {
        var res = await _likePost.DelMinusOneLikeAsync(likeCounter);
        return res;
    }
}