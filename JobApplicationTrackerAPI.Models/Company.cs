namespace JobApplicationTrackerAPI.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        // 1:N Company to Application
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
