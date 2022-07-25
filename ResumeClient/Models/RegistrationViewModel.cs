using System.ComponentModel.DataAnnotations;

namespace ResumeClient.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string SurName { get; set; }
        [Required]
        [Range(1, 100)]
        public int Age { get; set; }
    }
}
