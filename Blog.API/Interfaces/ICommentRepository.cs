using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Models;

namespace Blog.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(Comment comment);
        Task<Comment?> PatchAsync(int id, Comment partialComment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
