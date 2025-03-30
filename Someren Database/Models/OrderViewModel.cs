namespace Someren_Database.Models
{
	public class OrderViewModel
	{
		public Drink Drink { get; set; }

		// Propiedades necesarias para Drink		
		public string DrinkName { get; set; }

		// Propiedades necesarias para Objeto3		
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public OrderViewModel()
		{
			Drink = new Drink();
		}


	}
}
