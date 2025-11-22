using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models;

namespace Blog.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<Post?> UpdateAsync(Post post);
        Task<Post?> PatchAsync(int id, Post partialPost);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
