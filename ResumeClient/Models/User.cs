using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeClient.Models
{
    public class User : BaseEntity
    {      
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string SurName { get; set; }
        [Required]
        [Range(1, 100)]
        public int Age { get; set; }
        public List<MainSkill> MainSkills { get; set; }
        public List<AdditionSkill> AdditionSkills { get; set; }
        public string? Instagram { get; set; }
        public string? Telegram { get; set; }
        public string? GitHub { get; set; }
        public List<Section> Sections { get; set; }
        public List<Area> Areas { get; set; }
        public List<UserProject> Projects { get; set; }
        public string? BaseImage { get; set; }
        public int CredentialsId { get; set; }
    }
}
