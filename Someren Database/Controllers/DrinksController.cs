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

		[HttpGet]
		public ActionResult OrderDrinks(string lastNameFilter = null)
		{
            var students = _studentsRepository.ListStudents(lastNameFilter); 
            var drinks = _drinksRepository.ListDrinks();

            var studentForSelect = students.Select(student => new {
                student.StudentNumber,
                FullName = student.FirstName + " " + student.LastName
            });

            // Se crea el SelectList: el primer parámetro es la colección, 
            // el segundo es el nombre de la propiedad para el Value y el tercero para el Text.
            ViewBag.StudentsList = new SelectList(studentForSelect, "StudentNumber", "FullName");

            // Para las bebidas se supone que el objeto Drink tiene las propiedades DrinkId y Name.
            ViewBag.DrinksList = new SelectList(drinks, "DrinkId", "Name");

            return View();
		}


        [HttpPost]
		public ActionResult OrderDrinks(Order order)
		{			
			try
			{
				_drinksRepository.AddOrder(order);
				return RedirectToAction("DrinksIndex");
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}

			return View(order);
		}
    }
    
}
