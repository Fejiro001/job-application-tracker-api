using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public class ApplicationRepository
    {
        private readonly JobApplicationTrackerDbContext _context;
        public ApplicationRepository(JobApplicationTrackerDbContext context)
        {
            _context = context;
        }
        // Get all applications with additional filtering
        public async Task<List<Application>> GetFilteredApplicationsAsync(
            int? companyId,
            Status? status,
            decimal? minSalary,
            string? searchTerm,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Applications
                .Include(a => a.Company)
                .Include(a => a.Interviews)
                .Include(a => a.Skills)
                .AsQueryable();

            // Filter by a specific company
            if (companyId.HasValue)
            {
                query = query.Where(a => a.CompanyId == companyId);
            }

            // Filter by application status
            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status
                );
            }

            // Filter by minimum salary (if either min or max salary meets tthe requirement)
            if (minSalary.HasValue)
            {
                query = query.Where(a => a.SalaryMin >= minSalary.Value || a.SalaryMax >= minSalary.Value
                );
            }

            // Search applications by job title or company name
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                a.JobTitle.Contains(searchTerm) ||
                (a.Company != null && a.Company.Name.Contains(searchTerm)));
            }

            return await query
                .OrderByDescending(a => a.AppliedDate)
                .Skip((pageNumber - 1) * pageSize) // ignores the first set of calculated records, e.g., (2 - 1) * 10 = 10 records skipped
                .Take(pageSize) // returns 10 records
                .AsNoTracking()
                .ToListAsync();
        }

        // Get application by id with navigation details
        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.Company)
                .Include(a => a.Interviews)
                .Include(a => a.Skills)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ApplicationId == id);
        }

        // Create new application
        public async Task AddApplicationAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
        }

        // Update application
        public void UpdateApplication(Application application)
        {
            _context.Applications.Update(application);
        }

        // Update application status only
        public async Task<bool> UpdateApplicationStatusAsync(int id, Status newStatus)
        {
            Application? application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return false;
            }

            application.Status = newStatus;
            return true;
        }

        // Delete application by id
        public async Task DeleteApplicationAsync(int id)
        {
            Application? application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
