using JobApplicationTrackerAPI.BLL.DTOs;
using JobApplicationTrackerAPI.Models;

namespace JobApplicationTrackerAPI.BLL.Mappings
{
    public static class InterviewMappingExtensions
    {
        // Domain Entity -> Response DTO
        public static InterviewReadDto ToDto(this Interview interview)
        {
            return new InterviewReadDto
            {
                InterviewId = interview.InterviewId,
                StageName = interview.StageName,
                InterviewerName = interview.InterviewerName,
                Notes = interview.Notes,
                ScheduledAt = interview.ScheduledAt,
                IsCompleted = interview.IsCompleted,
                ApplicationId = interview.ApplicationId
            };
        }

        // Request DTO -> Domain Entity
        public static Interview ToEntity(this InterviewCreateDto dto)
        {
            return new Interview
            {
                ApplicationId = dto.ApplicationId,
                StageName = dto.StageName,
                InterviewerName = dto.InterviewerName,
                Notes = dto.Notes,
                ScheduledAt = dto.ScheduledAt,
                IsCompleted = dto.IsCompleted
            };
        }

        // List<Entity> -> IEnumerable<DTOs>
        public static IEnumerable<InterviewReadDto> ToDtoList(this IEnumerable<Interview> interviews)
        {
            return interviews.Select(i => i.ToDto());
        }
    }
}
