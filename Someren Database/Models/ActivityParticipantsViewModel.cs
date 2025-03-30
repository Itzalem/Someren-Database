namespace Someren_Database.Models
{
    public class ActivityParticipantsViewModel
    {
        public Activity Activity { get; set; }
        public List<Student> Participants { get; set; }
        public List<Student> NonParticipants { get; set; }

    }
}
