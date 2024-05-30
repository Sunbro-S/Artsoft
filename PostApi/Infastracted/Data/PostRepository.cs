using Domain.Entities;
using Domain.Interfaces;
using Medo;
using Microsoft.EntityFrameworkCore;

namespace Infastracted.Data;

public class PostRepository : IStorePost
{
    private readonly ApplicationDbContext _applicationDbContext;
    
    public PostRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task<Post[]> GetAllAsync()
    {
        var res = await _applicationDbContext.Posts.ToArrayAsync();

        return res;
    }

    public async Task<Guid> AddPost(Post post)
    {
        var postId = new Uuid7().ToGuid();
        post.Id = postId;
        await _applicationDbContext.Posts.AddAsync(post);
        await _applicationDbContext.SaveChangesAsync();
        return postId;
    }

    public async Task<string> DeletePostAsync(Guid id)
    {
        var post = await _applicationDbContext.Posts.FindAsync(id);
        if (post == null)
        {
            return "Пост не найден";
        }

        _applicationDbContext.Posts.Remove(post);
        await _applicationDbContext.SaveChangesAsync();
        return "Пост успешно удалён";
    }
}