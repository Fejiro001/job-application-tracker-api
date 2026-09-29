using JobApplicationTrackerAPI.BLL.Enums;
using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL.Mappings
{
    public static class ApplicationMappingExtensions
    {
        // Domain Entity -> Response DTO
        public static ApplicationReadDto ToDto(this Application application)
        {
            return new ApplicationReadDto
            {
                ApplicationId = application.ApplicationId,
                JobTitle = application.JobTitle,
                JobUrl = application.JobUrl,
                Status = (ApplicationStatus)application.Status,
                AppliedDate = application.AppliedDate,
                SalaryMin = application.SalaryMin,
                SalaryMax = application.SalaryMax,
                CompanyId = application.CompanyId,
                CompanyName = application.Company?.Name ?? string.Empty,
                CompanyLocation = application.Company?.Location ?? string.Empty,
                //Skills = application.Skills,
                //Interviews = application.Interviews
            };
        }

        // Request DTO -> Domain Entity
        public static Application ToEntity(this ApplicationCreateDto dto)
        {
            return new Application
            {
                CompanyId = dto.CompanyId,
                JobTitle = dto.JobTitle,
                JobUrl = dto.JobUrl,
                Status = (Status)dto.Status,
                SalaryMin = dto.SalaryMin,
                SalaryMax = dto.SalaryMax,
                AppliedDate = dto.AppliedDate
            };
        }

        // List<Entity> -> IEnumerable<DTOs>
        public static IEnumerable<ApplicationReadDto> ToDtoList(this IEnumerable<Application> applications)
        {
            return applications.Select(a => a.ToDto());
        }
    }
}
