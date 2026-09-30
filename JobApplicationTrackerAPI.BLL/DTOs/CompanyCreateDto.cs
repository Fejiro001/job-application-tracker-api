using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class CompanyCreateDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company website URL is required.")]
        [Url(ErrorMessage = "Please provide a valid website URL.")]
        [StringLength(2048)]
        public string WebsiteUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company industry is required.")]
        [StringLength(255)]
        public string Industry { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company location is required.")]
        [StringLength(255)]
        public string Location { get; set; } = string.Empty;
    }
}
