using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        // Assignment says author is hardcoded as "admin"
        [Required]
        public string Author { get; set; } = "admin";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // One Post -> Many Comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
