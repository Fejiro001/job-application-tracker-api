using JobApplicationTrackerAPI.BLL.Enums;

namespace JobApplicationTrackerAPI.BLL.DTOs
{
    public class ApplicationReadDto
    {
        public int ApplicationId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string JobUrl { get; set; } = string.Empty;
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }

        // Company Details
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyLocation { get; set; } = string.Empty;

        public List<string> Skills { get; set; } = new();
        public List<InterviewReadDto> Interviews { get; set; } = new();
    }
}
