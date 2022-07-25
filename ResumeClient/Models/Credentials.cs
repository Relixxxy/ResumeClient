using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{
    public class Credentials : BaseEntity
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Role { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public User User { get; set; }
    }
}
