using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class InterviewCreateDto
    {
        [Required(ErrorMessage = "Application is required.")]
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "Stage name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Stage name must be between 2 and 100 characters.")]
        public string StageName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? InterviewerName { get; set; }

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Scheduled date and time is required.")]
        public DateTime ScheduledAt { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
