using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.CreatedDate = DateTime.UtcNow;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(Comment comment)
        {
            var existing = await _context.Comments.FindAsync(comment.Id);
            if (existing == null)
                return null;

            existing.Name = comment.Name;
            existing.Email = comment.Email;
            existing.Content = comment.Content;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Comment?> PatchAsync(int id, Comment partial)
        {
            var existing = await _context.Comments.FindAsync(id);
            if (existing == null)
                return null;

            if (!string.IsNullOrWhiteSpace(partial.Name))
                existing.Name = partial.Name;

            if (!string.IsNullOrWhiteSpace(partial.Email))
                existing.Email = partial.Email;

            if (!string.IsNullOrWhiteSpace(partial.Content))
                existing.Content = partial.Content;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Comments.FindAsync(id);
            if (existing == null)
                return false;

            _context.Comments.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }
    }
}
