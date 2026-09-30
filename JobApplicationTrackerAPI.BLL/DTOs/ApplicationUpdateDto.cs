using JobApplicationTrackerAPI.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class ApplicationUpdateDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [Url]
        [StringLength(2048)]
        public string JobUrl { get; set; } = string.Empty;

        [Required]
        public ApplicationStatus Status { get; set; }

        [Range(0, 10000000)]
        public decimal? SalaryMin { get; set; }

        [Range(0, 10000000)]
        public decimal? SalaryMax { get; set; }
    }
}
