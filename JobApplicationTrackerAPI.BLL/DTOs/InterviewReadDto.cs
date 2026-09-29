using System.ComponentModel.DataAnnotations;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class InterviewReadDto
    {
        public int InterviewId { get; set; }
        public string StageName { get; set; } = string.Empty;
        public string? InterviewerName { get; set; }
        public string? Notes { get; set; }
        public DateTime ScheduledAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int ApplicationId { get; set; }
    }
}
