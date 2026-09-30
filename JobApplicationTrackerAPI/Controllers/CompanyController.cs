using JobApplicationTrackerAPI.BLL;
using JobApplicationTrackerAPI.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTrackerAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;
        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        // Retrieves all companies.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyReadDto>))]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return Ok(companies);
        }

        // Retrieves a specific company by ID.
        [HttpGet("{id:int}", Name = "GetCompanyById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();

            return Ok(company);
        }

        // Creates a new company.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto createDto)
        {
            var readDto = await _companyService.AddCompanyAsync(createDto);
            return CreatedAtRoute(nameof(GetCompanyById), new { id = readDto.CompanyId }, readDto);
        }
    }
}
