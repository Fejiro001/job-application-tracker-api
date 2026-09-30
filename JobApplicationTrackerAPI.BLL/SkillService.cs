using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.BLL.Mappings;
using JobApplicationTrackerAPI.DAL;

namespace JobApplicationTrackerAPI.BLL
{
    public class SkillService
    {
        private readonly SkillRepository _skillRepository;
        public SkillService(SkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        // Get all available skills
        public async Task<List<SkillReadDto>> GetSkillsAsync()
        {
            var skills = await _skillRepository.GetSkillsAsync();
            return skills.ToDtoList().ToList();
        }

        // Create a new unique skill
        public async Task<SkillReadDto> AddSkillAsync(SkillCreateDto createDto)
        {
            // Trim spaces
            var skillName = createDto.Name.Trim();

            // Check if skill already exists
            var existingSkills = await _skillRepository.GetSkillsAsync();
            bool nameExists = existingSkills.Any(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
            {
                throw new InvalidOperationException($"A skill with the name '{skillName}' already exists.");
            }

            var entity = createDto.ToEntity();
            entity.Name = skillName;

            await _skillRepository.AddSkillAsync(entity);
            await _skillRepository.SaveChangesAsync();

            return entity.ToDto();
        }

        // Add a new skill to the application
        public async Task<bool> AddSkillToApplicationAsync(int applicationId, int skillId)
        {
            bool added = await _skillRepository.AddSkillToApplicationAsync(applicationId, skillId);
            if (!added) return false;

            await _skillRepository.SaveChangesAsync();
            return true;
        }

        // Remove a skill from an application
        public async Task<bool> DeleteSkillFromApplicationAsync(int applicationId, int skillId)
        {
            bool removed = await _skillRepository.DeleteSkillFromApplicationAsync(applicationId, skillId);
            if (!removed) return false;

            await _skillRepository.SaveChangesAsync();
            return true;
        }
    }
}
