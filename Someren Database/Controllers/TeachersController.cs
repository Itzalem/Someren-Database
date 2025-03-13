using Microsoft.AspNetCore.Mvc;

namespace Someren_Database.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult TeachersIndex()
        {
            return View();
        }
    }
}
