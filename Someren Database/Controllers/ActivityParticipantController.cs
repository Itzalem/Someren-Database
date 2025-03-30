using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class ActivityParticipantController : Controller
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityParticipantController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ManageParticipants(int activityId)
        {
            var activity = await _activityRepository.GetActivityByIdAsync(activityId);
            if (activity == null) return NotFound();

            var participants = await _activityRepository.GetParticipantsAsync(activityId);
            var nonParticipants = await _activityRepository.GetNonParticipantsAsync(activityId);

            var viewModel = new ActivityParticipantsViewModel
            {
                Activity = activity,
                Participants = participants,
                NonParticipants = nonParticipants
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddParticipant(int activityId, int studentId)
        {
            await _activityRepository.AddParticipantAsync(activityId, studentId);
            return RedirectToAction("ManageParticipants", new { activityId });
        }

        public async Task<IActionResult> RemoveParticipant(int activityId, int studentId)
        {
            await _activityRepository.RemoveParticipantAsync(activityId, studentId);
            return RedirectToAction("ManageParticipants", new { activityId });
        }
    }
}
