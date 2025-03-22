using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult TeachersIndex(string sortOrder)
        {
            List<Teacher> teachers = _teachersRepository.ListTeachers();

            // Determine the order
            ViewBag.TeacherIDSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.RoomNumberSort = sortOrder == "room" ? "room_desc" : "room";
            ViewBag.FirstNameSort = sortOrder == "first" ? "first_desc" : "first";
            ViewBag.LastNameSort = sortOrder == "last" ? "last_desc" : "last";
            ViewBag.PhoneSort = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.AgeSort = sortOrder == "age" ? "age_desc" : "age";

            // Do the sorting
            switch (sortOrder)
            {
                case "id_desc":
                    teachers = teachers.OrderByDescending(t => t.TeacherID).ToList();
                    break;
                case "room":
                    teachers = teachers.OrderBy(t => t.RoomNumber).ToList();
                    break;
                case "room_desc":
                    teachers = teachers.OrderByDescending(t => t.RoomNumber).ToList();
                    break;
                case "first":
                    teachers = teachers.OrderBy(t => t.FirstName).ToList();
                    break;
                case "first_desc":
                    teachers = teachers.OrderByDescending(t => t.FirstName).ToList();
                    break;
                case "last":
                    teachers = teachers.OrderBy(t => t.LastName).ToList();
                    break;
                case "last_desc":
                    teachers = teachers.OrderByDescending(t => t.LastName).ToList();
                    break;
                case "phone":
                    teachers = teachers.OrderBy(t => t.PhoneNumber).ToList();
                    break;
                case "phone_desc":
                    teachers = teachers.OrderByDescending(t => t.PhoneNumber).ToList();
                    break;
                case "age":
                    teachers = teachers.OrderBy(t => t.Age).ToList();
                    break;
                case "age_desc":
                    teachers = teachers.OrderByDescending(t => t.Age).ToList();
                    break;
                default:
                    teachers = teachers.OrderBy(t => t.TeacherID).ToList();
                    break;
            }

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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return View(teacher);
            }
        }


    }
} 