namespace Someren_Database.Models
{
    public class ActivitySupervisorsViewModel
    {
        public Activity Activity { get; set; }
        public List<Teacher> Supervisors { get; set; }
        public List<Teacher> NotSupervisors { get; set; }

    }
}
