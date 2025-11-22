using Blog.Core.Interfaces;
using Blog.Core.Models;
using Blog.API.DTOs;
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
            var postExists = await _postRepository.ExistsAsync(postId);
            if (!postExists)
                return NotFound();

            var comments = await _commentRepository.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        // POST: /api/posts/{postId}/comments
        [HttpPost("posts/{postId}/comments")]
        public async Task<ActionResult<Comment>> CreateComment(int postId, Comment comment)
        {
            var postExists = await _postRepository.ExistsAsync(postId);
            if (!postExists)
                return NotFound();

            comment.PostId = postId;

            var createdComment = await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
        }

        // PUT: /api/comments/{id}
        [HttpPut("comments/{id}")]
        public async Task<ActionResult<Comment>> UpdateComment(int id, Comment updated)
        {
            if (id != updated.Id)
                return BadRequest("ID in URL must match ID in body");

            var result = await _commentRepository.UpdateAsync(updated);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PATCH: /api/comments/{id}
        [HttpPatch("comments/{id}")]
        public async Task<ActionResult<Comment>> PatchComment(int id, [FromBody] CommentPatchDto patch)
        {
            if (patch == null)
                return BadRequest();

        // Build a "partial" Comment object
        var partialComment = new Comment
        {
            Name = patch.Name ?? string.Empty,
            Email = patch.Email ?? string.Empty,
            Content = patch.Content ?? string.Empty
        };

        var result = await _commentRepository.PatchAsync(id, partialComment);

        if (result == null)
            return NotFound();

        return Ok(result);
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
