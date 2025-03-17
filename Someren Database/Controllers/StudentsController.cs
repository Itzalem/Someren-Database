using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudentsRepository _studentsRepository;

		public StudentsController(IStudentsRepository studentsRepository)
		{
			_studentsRepository = studentsRepository;
		}

		public IActionResult StudentsIndex()
		{
			List<Student> students = _studentsRepository.ListStudents();

			return View(students);
		}

		[HttpGet]
		public IActionResult AddStudent()
		{
			return View();
		}


		[HttpPost]
		public IActionResult AddStudent(Student student)
		{
			try
			{
				_studentsRepository.AddStudent(student);

				return RedirectToAction("StudentsIndex");
			}
			catch (Exception ex)
			{
				return View(student);
			}
		}


		// GET: UsersController/Edit/5
		[HttpGet]
		public ActionResult UpdateStudent(int? studentNumber)
		{
			if (studentNumber == null)
			{
				return NotFound();
			}

			Student? student = _studentsRepository.GetByStudentNumber((int)studentNumber);
			return View(student);
		}

		// POST: Users/Edit/5
		[HttpPost]
		public IActionResult UpdateStudent(Student student)
		{
			try
			{
				_studentsRepository.UpdateStudent(student);

				return RedirectToAction("StudentsIndex");
			}
			catch (Exception ex)
			{
				return View(student);
			}
		}

		[HttpGet]
		public ActionResult DeleteStudent(int? studentNumber)
		{
			if (studentNumber == null)
			{
				return NotFound();
			}

			Student? student = _studentsRepository.GetByStudentNumber((int)studentNumber);
			return View(student);
		}

		[HttpPost]
		public IActionResult DeleteStudent(Student student)
		{
			try
			{
				_studentsRepository.DeleteStudent(student);
				return RedirectToAction("StudentsIndex");
			}
			catch (Exception ex)
			{
				return View(student);
			}
		}

	
		//VIEW, ADD, CHANGE AND DELEYTE STUDENTS
		//Each field of the students can be changed
		//All student (management) functions are accessible to the user by using links/buttons;
	}
}
