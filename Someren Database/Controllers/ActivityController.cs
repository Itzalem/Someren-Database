using Microsoft.AspNetCore.Mvc;
using Someren_Database.Repositories;
using Someren_Database.Models;

namespace Someren_Database.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<IActionResult> ActivityIndex()
        {
            List<Someren_Database.Models.Activity> activities = await _activityRepository.GetAllActivitiesAsync();
            return View(activities);
        }

    }
}
