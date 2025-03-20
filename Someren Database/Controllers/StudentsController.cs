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

        



        //THINGS I NEED:
        //All students in list, order by last name
        //Display std#, fn, ln, cell#, class

        //VIEW, ADD, CHANGE AND DELEYTE STUDENTS
        //Each field of the students can be changed
        //All student (management) functions are accessible to the user by using links/buttons;
    }
}
