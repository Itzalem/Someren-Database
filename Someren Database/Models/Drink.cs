using Microsoft.AspNetCore.Components.Forms;

namespace Someren_Database.Models
{
	public class Drink
	{
		public int DrinkId { get; set; }
		public string Name { get; set; }
		public decimal Vat { get; set; }
		public int StockOfDrink { get; set; }

		public string Type { get; set; }


		public Drink()
		{
		}

		public Drink(int drinkId, string name, decimal vat, int stockOfDrink, string type)
		{
			DrinkId = drinkId;
			Name = name;
			Vat = vat;
			StockOfDrink = stockOfDrink;
			Type = type;
		}
	}
}
