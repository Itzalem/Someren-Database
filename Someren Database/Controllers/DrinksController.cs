﻿using Microsoft.AspNetCore.Mvc;
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
            ListsDropdown(lastNameFilter);

            return View();
        }

        [HttpPost]
        public ActionResult OrderDrinks(Order order, string lastNameFilter = null)
        {
            try
            {
                //to show the user the available amount without going back to the list
                int stock = _drinksRepository.GetStockById(order.DrinkId);
                Drink drink = _drinksRepository.GetDrinkById(order.DrinkId);

                if (ModelState.IsValid)
                {
                    if (order.Amount > stock)
                    {
                        ModelState.AddModelError("Amount", $"Sorry, not enough stock, {stock} {drink.Name}s  available at the moment");

                        ListsDropdown(lastNameFilter);

                        return View(order);
                    }

                    return RedirectToAction("ProcessOrder", new
                    {
                        studentNumber = order.StudentNumber,
                        drinkId = order.DrinkId,
                        amount = order.Amount
                    });

                }

                ListsDropdown(lastNameFilter);
                return View(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Sorry, unexpected error while processing the order, try again please.");

                ListsDropdown(lastNameFilter);
                return View();
            }
        }

        private void ListsDropdown(string lastNameFilter = null)
        {
            var students = _studentsRepository.ListStudents(lastNameFilter);
            var drinks = _drinksRepository.ListDrinks();

            var studentForSelect = students.Select(student => new {
                student.StudentNumber,
                FullName = student.FirstName + " " + student.LastName
            });

            ViewBag.StudentsList = new SelectList(studentForSelect, "StudentNumber", "FullName");
            ViewBag.DrinksList = new SelectList(drinks, "DrinkId", "Name");
        }

        [HttpGet]
        public ActionResult ProcessOrder(int studentNumber, int drinkId, int amount)
        {
            Order order = new Order
            {
                StudentNumber = studentNumber,
                DrinkId = drinkId,
                Amount = amount
            };

            Student student = _studentsRepository.GetByStudentNumber(studentNumber);

            Drink drink = _drinksRepository.GetDrinkById(drinkId);

            OrderStudentDrinkViewmodel viewModel = new OrderStudentDrinkViewmodel
            {
                Order = order,
                Drink = drink,
                Student = student
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ProcessOrder(OrderStudentDrinkViewmodel viewmodel)
        {
            try
            {
                _drinksRepository.AddOrder(viewmodel.Order);
                _drinksRepository.ReduceStock(viewmodel.Order, viewmodel.Drink);
                return RedirectToAction("OrderDrinks");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ModelState.AddModelError("", "Sorry, unexpected error while processing the order, try again please.");

                return View("OrderDrinks", viewmodel.Order);
            }
        }       
	}    
}
