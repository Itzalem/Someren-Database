using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetAllActivitiesAsync();

        Task<Activity> GetActivityByIdAsync(int activityId);
        Task<List<Student>> GetParticipantsAsync(int activityId);
        Task<List<Student>> GetNonParticipantsAsync(int activityId);
        Task AddParticipantAsync(int activityId, int studentId);
        Task RemoveParticipantAsync(int activityId, int studentId);
    }
}
