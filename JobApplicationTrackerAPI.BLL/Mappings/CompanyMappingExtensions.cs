using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL.Mappings
{
    public static class CompanyMappingExtensions
    {
        // Domain Entity -> Response DTO
        public static CompanyReadDto ToDto(this Company company)
        {
            return new CompanyReadDto
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Location = company.Location,
                Industry = company.Industry,
                WebsiteUrl = company.WebsiteUrl,
                ApplicationCount = company.Applications?.Count ?? 0
            };
        }

        // Request DTO -> Domain Entity
        public static Company ToEntity(this CompanyCreateDto dto)
        {
            return new Company
            {
                Name = dto.Name,
                Location = dto.Location,
                Industry = dto.Industry,
                WebsiteUrl = dto.WebsiteUrl
            };
        }

        // List<Entity> -> IEnumerable<DTOs>
        public static IEnumerable<CompanyReadDto> ToDtoList(this IEnumerable<Company> companies)
        {
            return companies.Select(c => c.ToDto());
        }
    }
}
