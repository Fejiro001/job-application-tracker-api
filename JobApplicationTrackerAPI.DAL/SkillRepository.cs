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

        // Create a skill
        public async Task AddSkillAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
        }

        // Delete skill by id
        public async Task DeleteSkillAsync(int id)
        {
            Skill? skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
