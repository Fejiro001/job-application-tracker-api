namespace JobApplicationTrackerAPI.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        // M:N Application to Skill
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
