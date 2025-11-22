using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property back to Post
        public Post? Post { get; set; }
    }
}
