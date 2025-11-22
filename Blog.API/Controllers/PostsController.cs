using Blog.Core.Interfaces;
using Blog.API.DTOs;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: /api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postRepository.GetAllAsync();
            return Ok(posts);
        }

        // GET: /api/posts/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                {
                    return NotFound();
                }
            return Ok(post);
        }

        // POST: /api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Author is always "admin"
            post.Author = "admin";

            var created = await _postRepository.CreateAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = created.Id }, created);
        }

        // PUT: /api/posts/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest("Id mismatch between URL and body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _postRepository.UpdateAsync(post);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Post>> PatchPost(int id, [FromBody] PostPatchDto patch)
        {
            // Get existing post
            var existing = await _postRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Only update fields that were sent
            if (!string.IsNullOrWhiteSpace(patch.Title))
                existing.Title = patch.Title;

            if (!string.IsNullOrWhiteSpace(patch.Content))
                existing.Content = patch.Content;

            existing.UpdatedDate = DateTime.UtcNow;

            // Re-use your existing update logic
            var updated = await _postRepository.UpdateAsync(existing);
            return Ok(updated);
        }


        // DELETE: /api/posts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _postRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
