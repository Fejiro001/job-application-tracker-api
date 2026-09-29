using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public class InterviewRepository
    {
        private readonly JobApplicationTrackerDbContext _context;
        public InterviewRepository(JobApplicationTrackerDbContext context)
        {
            _context = context;
        }

        // Add a new interview to the application
        public async Task<bool> AddInterviewToApplicationAsync(int applicationId, Interview interview)
        {
            // Verify application exists
            bool applicationExists = await _context.Applications.AnyAsync(a => a.ApplicationId == applicationId);
            if (!applicationExists)
            {
                return false;
            }

            interview.ApplicationId = applicationId;
            await _context.Interviews.AddAsync(interview);
            return true;
        }

        // Get all interview rounds for a specific application
        public async Task<List<Interview>> GetInterviewsByApplicationAsync(int applicationId)
        {
            return await _context.Interviews
                .Where(i => i.ApplicationId == applicationId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
