using JobApplicationTrackerAPI.BLL;
using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;

        public ApplicationController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // Retrieve applications with sorting, filtering and pagination available
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ApplicationReadDto>))]
        public async Task<IActionResult> GetApplications(
            [FromQuery] int? companyId,
            [FromQuery] Status? status,
            [FromQuery] decimal? minSalary,
            [FromQuery] string? searchTerm,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var applications = await _applicationService.GetFilteredApplicationsAsync(
                companyId, status, minSalary, searchTerm, pageNumber, pageSize);

            return Ok(applications);
        }

        // Retrieve an application by its id
        [HttpGet("{id:int}", Name = nameof(GetApplicationById))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            if (application == null)
            {
                return NotFound(new { message = "Application was not found." });
            }

            return Ok(application);
        }

        // Create a new application
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApplicationReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationCreateDto createDto)
        {
            try
            {
                var createdApplication = await _applicationService.AddApplicationAsync(createDto);

                return CreatedAtRoute(
                    nameof(GetApplicationById),
                    new { id = createdApplication.ApplicationId },
                    createdApplication);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update an existing application
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] ApplicationUpdateDto updateDto)
        {
            try
            {
                bool updated = await _applicationService.UpdateApplication(id, updateDto);
                if (!updated)
                {
                    return NotFound(new { message = "Application was not found." });
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Update an applications status
        [HttpPatch("{id:int}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplicationStatus(int id, [FromBody] Status newStatus)
        {
            bool updated = await _applicationService.UpdateApplicationStatusAsync(id, newStatus);
            if (!updated)
            {
                return NotFound(new { message = "Application was not found." });
            }

            return NoContent();
        }

        // Delete an existing application
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            bool deleted = await _applicationService.DeleteApplicationAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Application was not found." });
            }

            return NoContent();
        }
    }
}
