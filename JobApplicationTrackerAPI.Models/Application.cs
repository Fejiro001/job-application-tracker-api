namespace JobApplicationTrackerAPI.Models
{
    public enum Status
    {
        Saved,
        Applied, 
        Screening,
        Interviewing,
        Offered, 
        Rejected
    }
    public class Application
    {
        public int ApplicationId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string JobUrl { get; set; } = string.Empty;
        public Status Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        // 1:N User to Application
        public int UserId { get; set; }
        public User? User { get; set; }
        // 1:N Company to Application
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        // 1:N Application to Interview
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
