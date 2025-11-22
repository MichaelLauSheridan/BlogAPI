using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Core.Interfaces;
using Blog.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        // GET: /api/comments
        [HttpGet("comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments);
        }

        // GET: /api/comments/{id}
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // GET: /api/posts/{postId}/comments
        [HttpGet("posts/{postId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForPost(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return NotFound();

            var comments = await _commentRepository.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        // POST: /api/posts/{postId}/comments
        [HttpPost("posts/{postId}/comments")]
        public async Task<ActionResult<Comment>> CreateComment(int postId, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return NotFound();

            comment.PostId = postId;

            var created = await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = created.Id }, created);
        }

        // PUT: /api/comments/{id}
        [HttpPut("comments/{id}")]
        public async Task<ActionResult<Comment>> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
                return BadRequest("Id in URL and body must match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _commentRepository.UpdateAsync(comment);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // PATCH: /api/comments/{id}
        [HttpPatch("comments/{id}")]
        public async Task<ActionResult<Comment>> PatchComment(int id, [FromBody] Comment partialComment)
        {
            var patched = await _commentRepository.PatchAsync(id, partialComment);
            if (patched == null)
                return NotFound();

            return Ok(patched);
        }

        // DELETE: /api/comments/{id}
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleted = await _commentRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
