using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Someren_Database.Models;
using Someren_Database.ViewModels;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class DrinksController : Controller
    {

		private readonly IDrinksRepository _drinksRepository;
		private readonly IStudentsRepository _studentsRepository;

		public DrinksController(IDrinksRepository drinksRepository, IStudentsRepository studentsRepository)
		{
			_drinksRepository = drinksRepository;
			_studentsRepository = studentsRepository;
		}

		public IActionResult DrinksIndex(string lastNameFilter = null)
        {
			List<Drink> drinks = _drinksRepository.ListDrinks();
			List<Student> students = _studentsRepository.ListStudents(lastNameFilter);

			Drink_StudentViewModel viewModel = new Drink_StudentViewModel
			{
				Drinks = drinks,
				Students = students
			};

			return View(viewModel);
        }
    }
}
