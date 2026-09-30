using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL.Mappings
{
    public static class SkillMappingExtensions
    {
        // Domain Entity -> Response DTO
        public static SkillReadDto ToDto(this Skill skill)
        {
            return new SkillReadDto
            {
                SkillId = skill.SkillId,
                Name = skill.Name,
                Category = skill.Category
            };
        }

        // Request DTO -> Domain Entity
        public static Skill ToEntity(this SkillCreateDto dto)
        {
            return new Skill
            {
                Name = dto.Name,
                Category = dto.Category
            };
        }

        // List<Entity> -> IEnumerable<DTOs>
        public static IEnumerable<SkillReadDto> ToDtoList(this IEnumerable<Skill> skills)
        {
            return skills.Select(s => s.ToDto());
        }
    }
}
