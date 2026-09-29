using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.BLL.Mappings;
using JobApplicationTrackerAPI.DAL;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL
{
    public class CompanyService
    {
        private readonly CompanyRepository _companyRepository;
        public CompanyService(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        // Get all companies
        public async Task<List<CompanyReadDto>> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            return companies.ToDtoList().ToList();
        }

        // Get company by id
        public async Task<CompanyReadDto?> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);
            return company?.ToDto();
        }

        // Create company
        public async Task<CompanyReadDto> AddCompanyAsync(CompanyCreateDto createDto)
        {
            var entity = createDto.ToEntity();

            await _companyRepository.AddCompanyAsync(entity);
            await _companyRepository.SaveChangesAsync();

            return entity.ToDto();
        }
    }
}
