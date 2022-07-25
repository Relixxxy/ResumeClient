using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{
    public class UserProject : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public string Link { get; set; }
        public string? BaseImage { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
    }
}
