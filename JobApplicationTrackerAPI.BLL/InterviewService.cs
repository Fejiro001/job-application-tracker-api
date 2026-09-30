using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.BLL.Mappings;
using JobApplicationTrackerAPI.DAL;

namespace JobApplicationTrackerAPI.BLL
{
    public class InterviewService
    {
        private readonly InterviewRepository _interviewRepository;
        public InterviewService(InterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }
        // Add a new interview to the application
        public async Task<InterviewReadDto> AddInterviewToApplicationAsync(int applicationId, InterviewCreateDto createDto)
        {
            createDto.ApplicationId = applicationId;
            var entity = createDto.ToEntity();

            await _interviewRepository.AddInterviewToApplicationAsync(applicationId, entity);
            await _interviewRepository.SaveChangesAsync();

            return entity.ToDto();
        }

        // Get all interview rounds for a specific application
        public async Task<List<InterviewReadDto>> GetInterviewsByApplicationAsync(int applicationId)
        {
            var interviews = await _interviewRepository.GetInterviewsByApplicationAsync(applicationId);
            return interviews.ToDtoList().ToList();
        }
    }
}
