using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public class SkillRepository
    {
        private readonly JobApplicationTrackerDbContext _context;
        public SkillRepository(JobApplicationTrackerDbContext context)
        {
            _context = context;
        }
        // Get all skills
        public async Task<List<Skill>> GetSkillsAsync()
        {
            return await _context.Skills
                .Include(c => c.Applications)
                .AsNoTracking()
                .ToListAsync();
        }

        // Create a new skill
        public async Task AddSkillAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
        }

        // Add a new skill to the application
        public async Task<bool> AddSkillToApplicationAsync(int applicationId, int skillId)
        {
            // Fetch the application with its current skills
            Application? application = await _context.Applications
                .Include(a => a.Skills)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
            if (application == null)
            {
                return false;
            }

            // Check skill exists in database
            Skill? skill = await _context.Skills.FindAsync(skillId);
            if (skill == null)
            {
                return false;
            }

            // Check if application has the skill already
            if (application.Skills.Any(s => s.SkillId == skillId))
            {
                throw new InvalidOperationException("Skill is already attached to this application");
            }

            application.Skills.Add(skill);
            return true;
        }

        // Remove a skill from an application
        public async Task<bool> DeleteSkillFromApplicationAsync(int applicationId, int skillId)
        {
            Application? application = await _context.Applications
                .Include(a => a.Skills)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);

            if (application == null)
            {
                return false;
            }


            Skill? skill = application.Skills.FirstOrDefault(s => s.SkillId == skillId);
            if (skill == null)
            {
                return false;
            }

            application.Skills.Remove(skill);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
