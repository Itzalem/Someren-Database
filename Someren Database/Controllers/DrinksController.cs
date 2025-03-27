using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class DrinksController : Controller
    {

		private readonly IDrinksRepository _drinksRepository;

		public DrinksController(IDrinksRepository drinksRepository)
		{
			_drinksRepository = drinksRepository;
		}

		public IActionResult DrinksIndex()
        {
			List<Drink> drinks = _drinksRepository.ListDrinks();
			return View(drinks);
        }
    }
}
