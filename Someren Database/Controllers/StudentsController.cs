using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudentsRepository _studentsRepository;
		private readonly IRoomRepository _roomRepository;

		public StudentsController(IStudentsRepository studentsRepository, IRoomRepository roomRepository)
		{
			_studentsRepository = studentsRepository;
			_roomRepository = roomRepository;
		}

		[HttpGet]
		public IActionResult StudentsIndex(string lastNameFilter = null)
		{
			List<Student> students = _studentsRepository.ListStudents(lastNameFilter);
			ViewBag.LastNameFilter = lastNameFilter; 

			return View(students);
		}

		[HttpGet]
		public async Task<IActionResult> AddStudent()
		{
			
			IEnumerable<Room> allRooms = await _roomRepository.GetAllRoomsAsync();
			
			List<Room> studentRooms = new List<Room>();
			
			foreach (Room r in allRooms)
			{
				if (r.RoomType != null && r.RoomType.Equals("Student", System.StringComparison.OrdinalIgnoreCase))
				{
					studentRooms.Add(r);
				}
			}

			ViewBag.RoomId = new SelectList(studentRooms, "RoomNumber", "RoomNumber");

			return View(new Student());
		}


		[HttpPost]
		public async Task<IActionResult> AddStudent(Student student)
		{
			if (ModelState.IsValid)
			{
				var existingStudent = _studentsRepository.GetByStudentNumber(student.StudentNumber);
				if (existingStudent != null)
				{
					ModelState.AddModelError("StudentNumber", "That number was already assigned to a student, please choose a new one");
				}
				else
				{
					try
					{
						_studentsRepository.AddStudent(student);
						return RedirectToAction("StudentsIndex");
					}
					catch (Exception ex)
					{
						ModelState.AddModelError("", ex.Message);
					}
				}
			}

			IEnumerable<Room> allRooms = await _roomRepository.GetAllRoomsAsync();

			List<Room> studentRooms = new List<Room>();
			foreach (Room r in allRooms)
			{
				if (r.RoomType != null && r.RoomType.Equals("Student", System.StringComparison.OrdinalIgnoreCase))
				{
					studentRooms.Add(r);
				}
			}			

			ViewBag.RoomId = new SelectList(studentRooms, "RoomNumber", "RoomNumber", student.RoomNumber);

			return View(student);
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
		
	}
}
