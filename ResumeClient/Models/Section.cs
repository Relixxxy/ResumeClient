using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{
    public class Section : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Color { get; set; }
        public int UserId { get; set; }
    }
}
