using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;
using System;
using System.Threading.Tasks;

namespace Someren_Database.Controllers
{
    public class ActivityParticipantController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IStudentsRepository _studentRepository;

        public ActivityParticipantController(IActivityRepository activityRepository, IStudentsRepository studentsRepositor)
        {
            _activityRepository = activityRepository;
            _studentRepository = studentsRepositor;
        }

        [HttpGet]
        public async Task<IActionResult> ManageParticipants(int activityId)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> AddParticipant(int activityId, int studentId)
        {
            try
            {
                var student = _studentRepository.GetByStudentNumber(studentId);
                await _activityRepository.AddParticipantAsync(activityId, studentId);
                TempData["Message"] = $"Student {student.FirstName} {student.LastName} was successfully added.";
                return RedirectToAction("ManageParticipants", new { activityId });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> RemoveParticipant(int activityId, int studentId)
        {
            try
            {
                var student = _studentRepository.GetByStudentNumber(studentId);
                await _activityRepository.RemoveParticipantAsync(activityId, studentId);
                TempData["Message"] = $"Student {student.FirstName} {student.LastName} was successfully removed.";
                return RedirectToAction("ManageParticipants", new { activityId });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
