using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class SkillCreateDto
    {
        [Required(ErrorMessage = "Skill name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Skill name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string Category { get; set; } = string.Empty;
    }
}
