using System;
using System.ComponentModel.DataAnnotations;

namespace MovieCommentsApi.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string? UserId { get; set; }

        [Required]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
