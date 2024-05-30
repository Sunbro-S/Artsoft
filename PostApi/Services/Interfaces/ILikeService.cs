using Domain.Entities;

namespace Services.Interfaces;

public interface ILikeService
{
    Task<int> GetLikesCountServiceAsync(LikeCounter likeCounter, Post post);

    Task<int> AddPlusOneLikeServiceAsync(LikeCounter likeCounter);

    Task<int> DelMinusOneLikeServiceAsync(LikeCounter likeCounter);
}