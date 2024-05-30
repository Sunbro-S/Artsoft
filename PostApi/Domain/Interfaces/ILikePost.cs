using Domain.Entities;

namespace Domain.Interfaces;

public interface ILikePost
{
    Task<int> GetLikesCountAsync(LikeCounter likeCounter, Post post);

    Task<int> AddPlusOneLikeAsync(LikeCounter likeCounter);

    Task<int> DelMinusOneLikeAsync(LikeCounter likeCounter);
}