using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeachersRepository _teachersRepository;

        public TeachersController(ITeachersRepository teachersRepository)
        {
            _teachersRepository = teachersRepository;
        }

        public IActionResult TeachersIndex()
        {
            List<Teacher> teachers = _teachersRepository.ListTeachers();

            return View(teachers);
        }

        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            try
            {
                _teachersRepository.AddTeacher(teacher);

                return RedirectToAction("TeachersIndex");
            }
            catch (Exception ex)
            {
                return View(teacher);
            }
        }


        [HttpGet]
        public ActionResult UpdateTeacher(int? teacherID)
        {
            if (teacherID == null)
            {
                return NotFound();
            }

            Teacher? teacher = _teachersRepository.GetByTeacherID((int)teacherID);
            return View(teacher);
        }

      
        [HttpPost]
        public IActionResult UpdateTeacher(Teacher teacher)
        {
            try
            {
                _teachersRepository.UpdateTeacher(teacher);

                return RedirectToAction("TeachersIndex");
            }
            catch (Exception ex)
            {
                return View(teacher);
            }
        }

        [HttpGet]
        public ActionResult DeleteTeacher(int? teacherID)
        {
            if (teacherID == null)
            {
                return NotFound();
            }

            Teacher? teacher = _teachersRepository.GetByTeacherID((int)teacherID);
            return View(teacher);
        }

        [HttpPost]
        public IActionResult DeleteTeacher(Teacher teacher)
        {
            try
            {
                _teachersRepository.DeleteTeacher(teacher);
                return RedirectToAction("TeachersIndex");
            }
            catch (Exception ex)
            {
                return View(teacher);
            }
        }


    }
}
