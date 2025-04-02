namespace Someren_Database.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public int StudentNumber { get; set; }
		public int DrinkId { get; set; }
		public int Amount { get; set; }

		public Order ()
		{ }

		public Order(int orderId, int studentNumber, int drinkId, int amount)
		{
			OrderId = orderId;
			StudentNumber = studentNumber;
			DrinkId = drinkId;
			Amount = amount;
		}
	}
}
