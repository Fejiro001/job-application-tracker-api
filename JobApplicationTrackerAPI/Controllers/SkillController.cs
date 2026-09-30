using JobApplicationTrackerAPI.BLL;
using JobApplicationTrackerAPI.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }

        // Retrive all skills
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SkillReadDto>))]
        public async Task<IActionResult> GetSkills()
        {
            var skills = await _skillService.GetSkillsAsync();
            return Ok(skills);
        }

        // Create a new unique skill
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SkillReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateSkill([FromBody] SkillCreateDto createDto)
        {
            try
            {
                var readDto = await _skillService.AddSkillAsync(createDto);
                return CreatedAtRoute(nameof(GetSkills), null, readDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // Add a new skill to an application
        [HttpPost("application/{applicationId:int}/skills/{skillId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddSkillToApplication(int applicationId, int skillId)
        {
            try
            {
                bool added = await _skillService.AddSkillToApplicationAsync(applicationId, skillId);
                if (!added)
                {
                    return NotFound(new { message = "Application or Skill not found." });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // Remove a skill from an  application
        [HttpDelete("application/{applicationId:int}/skills/{skillId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSkillFromApplication(int applicationId, int skillId)
        {
            bool removed = await _skillService.DeleteSkillFromApplicationAsync(applicationId, skillId);
            if (!removed)
            {
                return NotFound(new { message = "Application or Skill association not found." });
            }

            return NoContent();
        }
    }
}
