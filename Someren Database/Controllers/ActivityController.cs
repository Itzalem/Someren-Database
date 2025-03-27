using Microsoft.AspNetCore.Mvc;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<IActionResult> ActivityIndex(string searchName)
        {
            var activities = await _activityRepository.GetAllActivitesAsync();

            if (!string.IsNullOrEmpty(searchName))
            {
                activities = activities.Where(a => a.ActivityName.Contains(searchName)).ToList();
            }

            return View(activities);
        }
}
}
