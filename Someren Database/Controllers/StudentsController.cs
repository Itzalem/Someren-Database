using Microsoft.AspNetCore.Mvc;

namespace Someren_Database.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult StudentsIndex()
        {
            return View();
        }
    }
}
