using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            post.CreatedDate = DateTime.UtcNow;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            var existing = await _context.Posts.FindAsync(post.Id);
            if (existing == null)
            {
                return null;
            }

            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<Post?> PatchAsync(int id, Post partialPost)
        {
            var existing = await _context.Posts.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(partialPost.Title))
            {
                existing.Title = partialPost.Title;
            }

            if (!string.IsNullOrWhiteSpace(partialPost.Content))
            {
                existing.Content = partialPost.Content;
            }

            existing.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Posts.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _context.Posts.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Posts.AnyAsync(p => p.Id == id);
        }
    }
}
