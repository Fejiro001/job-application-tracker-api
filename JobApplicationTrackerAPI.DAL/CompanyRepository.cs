using JobApplicationTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.DAL
{
    public class CompanyRepository
    {
        private readonly JobApplicationTrackerDbContext _context;
        public CompanyRepository(JobApplicationTrackerDbContext context)
        {
            _context = context;
        }
        // Get all companies
        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.Applications)
                .AsNoTracking()
                .ToListAsync();
        }

        // Get company by id
        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        // Create company
        public async Task AddCompanyAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
