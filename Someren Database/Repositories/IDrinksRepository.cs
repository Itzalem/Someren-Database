using Someren_Database.Models;

namespace Someren_Database.Repositories
{
	public interface IDrinksRepository
	{
		List<Drink> ListDrinks();
		void AddOrder(Order order); 
	}
}
