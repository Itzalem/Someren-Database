using Microsoft.AspNetCore.Mvc;

namespace Someren_Database.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult StudentsIndex()
        {
            return View();
        }

        



        //THINGS I NEED:
        //All students in list, order by last name
        //Display std#, fn, ln, cell#, class

        //VIEW, ADD, CHANGE AND DELEYTE STUDENTS
        //Each field of the students can be changed
        //All student (management) functions are accessible to the user by using links/buttons;
    }
}
