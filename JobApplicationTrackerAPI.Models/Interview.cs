namespace JobApplicationTrackerAPI.Models
{
    public class Interview
    {
        public int InterviewId { get; set; }
        public string StageName { get; set; } = string.Empty;
        public string? InterviewerName { get; set; }
        public string? Notes { get; set; }
        public DateTime ScheduledAt { get; set; }
        public bool IsCompleted { get; set; } = false;

        // 1:N Application to Interview
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }
    }
}
