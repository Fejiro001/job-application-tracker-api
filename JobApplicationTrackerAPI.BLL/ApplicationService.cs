using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.BLL.Mappings;
using JobApplicationTrackerAPI.DAL;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL
{
    public class ApplicationService
    {
        private readonly ApplicationRepository _applicationRepository;
        public ApplicationService(ApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        // Retrieve all ApplicationReadDto
        public async Task<List<ApplicationReadDto>> GetFilteredApplicationsAsync(
            int? companyId,
            Status? status,
            decimal? minSalary,
            string? searchTerm,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var apps = await _applicationRepository.GetFilteredApplicationsAsync(companyId, status, minSalary, searchTerm, pageNumber, pageSize);

            // Converts IEnumerable<Application> -> List<ApplicationReadDto>
            return apps.ToDtoList().ToList();
        }

        // Get ApplicationReadDto by id with navigation details
        public async Task<ApplicationReadDto?> GetApplicationByIdAsync(int id)
        {
            var app = await _applicationRepository.GetApplicationByIdAsync(id);
            if (app == null) return null;

            return app.ToDto();
        }

        // Accepts ApplicationCreateDto, map to entity, save, and return ApplicationReadDto
        public async Task<ApplicationReadDto> AddApplicationAsync(ApplicationCreateDto createDto)
        {
            ValidateSalary(createDto.SalaryMin, createDto.SalaryMax);

            var entity = createDto.ToEntity();

            await _applicationRepository.AddApplicationAsync(entity);
            await _applicationRepository.SaveChangesAsync();

            var createdEntity = await _applicationRepository.GetApplicationByIdAsync(entity.ApplicationId);
            // Null forgiving -> !.
            return createdEntity!.ToDto();
        }

        // Accepts ApplicationUpdateDto and ID to update the application
        public async Task<bool> UpdateApplication(int id, ApplicationUpdateDto updateDto)
        {
            ValidateSalary(updateDto.SalaryMin, updateDto.SalaryMax);

            var existingApp = await _applicationRepository.GetApplicationByIdAsync(id);
            if (existingApp == null) return false;

            // Map updated fields from DTO onto existing entity
            existingApp.CompanyId = updateDto.CompanyId;
            existingApp.JobTitle = updateDto.JobTitle;
            existingApp.JobUrl = updateDto.JobUrl;
            existingApp.Status = (Status)updateDto.Status;
            existingApp.SalaryMin = updateDto.SalaryMin;
            existingApp.SalaryMax = updateDto.SalaryMax;

            _applicationRepository.UpdateApplication(existingApp);
            await _applicationRepository.SaveChangesAsync();
            return true;
        }

        // Update application status only
        public async Task<bool> UpdateApplicationStatusAsync(int id, Status newStatus)
        {
            var existingApp = await _applicationRepository.GetApplicationByIdAsync(id);
            if (existingApp == null) return false;

            await _applicationRepository.UpdateApplicationStatusAsync(id, newStatus);
            await _applicationRepository.SaveChangesAsync();
            return true;
        }

        // Delete application by id
        public async Task<bool> DeleteApplicationAsync(int id)
        {
            var existingApp = await _applicationRepository.GetApplicationByIdAsync(id);
            if (existingApp == null) return false;

            await _applicationRepository.DeleteApplicationAsync(id);
            await _applicationRepository.SaveChangesAsync();
            return true;
        }

        private static void ValidateSalary(decimal? min, decimal? max)
        {
            if (min.HasValue && max.HasValue && min > max)
            {
                throw new ArgumentException("Minimum salary cannot exceed maximum salary.");
            }
        }
    }
}
