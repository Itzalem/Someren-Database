using Microsoft.AspNetCore.Mvc;

namespace Someren_Database.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult RoomsIndex()
        {
            return View();
        }
    }
}
