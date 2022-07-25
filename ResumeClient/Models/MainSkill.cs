using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{
    public class MainSkill : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Color { get; set; }
        public int UserId { get; set; }
    }
}
