using Someren_Database.Models;

namespace Someren_Database.ViewModels
{
    public class OrderStudentDrinkViewmodel
    {
        public Order Order { get; set; }
        public Drink Drink { get; set; }
        public Student Student { get; set; }

        

    }
}
/*public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }*/