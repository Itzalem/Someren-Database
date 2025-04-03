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