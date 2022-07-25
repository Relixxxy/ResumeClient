using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{ 
    public class AdditionSkill : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Color { get; set; }
        public int UserId { get; set; }
    }
}
