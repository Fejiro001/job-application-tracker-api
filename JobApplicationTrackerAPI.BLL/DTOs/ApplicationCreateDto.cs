using JobApplicationTrackerAPI.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class ApplicationCreateDto
    {
        [Required(ErrorMessage = "Company is required.")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Job title must be between 2 and 100 characters.")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [StringLength(2048, MinimumLength = 10, ErrorMessage = "Job URL must be a valid link.")]
        public string JobUrl { get; set; } = string.Empty;

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        [Required]
        public DateTime AppliedDate { get; set; }

        [Range(0, 10000000, ErrorMessage = "Minimum salary must be a positive number.")]
        public decimal? SalaryMin { get; set; }

        [Range(0, 10000000, ErrorMessage = "Maximum salary must be a positive number.")]
        public decimal? SalaryMax { get; set; }
    }
}
