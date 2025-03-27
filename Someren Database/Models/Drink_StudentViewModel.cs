using Someren_Database.Models;

namespace Someren_Database.ViewModels
{
	public class Drink_StudentViewModel
	{
		public IEnumerable<Student> Students { get; set; }
		public IEnumerable<Drink> Drinks { get; set; }

	}
}
