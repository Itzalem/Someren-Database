using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;
using System;
using System.Threading.Tasks;

namespace Someren_Database.Controllers
{
    public class ActivitySupervisorController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ITeachersRepository _teacherRepository;

        public ActivitySupervisorController(IActivityRepository activityRepository, ITeachersRepository teacherRepository)
        {
            _activityRepository = activityRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ManageSupervisors(int activityId)
        {
            try
            {
                var act = await _activityRepository.GetActivityByIdAsync(activityId);
                if (act == null) return NotFound();

                var supervisors = await _activityRepository.GetSupervisorsAsync(activityId);
                var notSupervisors = await _activityRepository.GetNotSupervisorsAsync(activityId);

                var viewModel = new ActivitySupervisorsViewModel
                {
                    Activity = act,
                    Supervisors = supervisors,
                    NotSupervisors = notSupervisors
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> AddSupervisor(int activityId, int teacherID)
        {
            try
            {
                var teacher = _teacherRepository.GetByTeacherID(teacherID);
                await _activityRepository.AddSupervisorAsync(activityId, teacherID);
                return RedirectToAction("ManageSupervisors", new { activityId });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> RemoveSupervisor(int activityId, int teacherID)
        {
            try
            {
                var teacher = _teacherRepository.GetByTeacherID(teacherID);
                await _activityRepository.RemoveSupervisorAsync(activityId, teacherID);
                return RedirectToAction("ManageSupervisors", new { activityId });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
