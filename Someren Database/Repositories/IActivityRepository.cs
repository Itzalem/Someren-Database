using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitesAsync();
    }
}
