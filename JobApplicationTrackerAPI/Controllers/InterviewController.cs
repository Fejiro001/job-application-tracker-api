using JobApplicationTrackerAPI.BLL;
using JobApplicationTrackerAPI.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class InterviewController : ControllerBase
    {
        private readonly InterviewService _interviewService;

        public InterviewController(InterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        // Retrieve all interrviews linked to an application
        // GET: api/interview/application/5
        [HttpGet("application/{applicationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InterviewReadDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInterviewsByApplication(int applicationId)
        {
            var interviews = await _interviewService.GetInterviewsByApplicationAsync(applicationId);
            if (interviews == null) return NotFound();

            return Ok(interviews);
        }

        // Create a new interview for application
        // POST: api/interview/application/5
        [HttpPost("application/{applicationId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InterviewReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddInterviewToApplication(int applicationId, [FromBody] InterviewCreateDto createDto)
        {
            var readDto = await _interviewService.AddInterviewToApplicationAsync(applicationId, createDto);

            return CreatedAtAction(
                nameof(GetInterviewsByApplication),
                new { applicationId = readDto.ApplicationId },
                readDto
            );
        }
    }
}
