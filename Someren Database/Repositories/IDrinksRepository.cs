using Someren_Database.Models;

namespace Someren_Database.Repositories
{
	public interface IDrinksRepository
	{
		List<Drink> ListDrinks();
		Drink GetDrinkById(int drinkId);
        
		void AddOrder(Order order); 
		void ReduceStock(Order order, Drink drink);
	}
}
