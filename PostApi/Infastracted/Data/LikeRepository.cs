using Domain.Entities;
using Domain.Interfaces;

namespace Infastracted.Data;

public class LikeRepository : ILikePost
{
    public async Task<int> GetLikesCountAsync(LikeCounter likeCounter, Post post)
    {
        return post.LikeCount = likeCounter.LikeCount;
    }

    public async Task<int> AddPlusOneLikeAsync(LikeCounter likeCounter)
    {
        return likeCounter.LikeCount + 1;
    }

    public async Task<int> DelMinusOneLikeAsync(LikeCounter likeCounter)
    {
        return likeCounter.LikeCount - 1;
    }
}