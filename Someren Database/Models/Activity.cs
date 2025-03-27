using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Someren_Database.Models
{
    public class Activity
    {
        [Key]
        [Column("activity_id")]
        public int ActivityId { get; set; }
        
        [Column("activity_name")]
        public string ActivityName { get; set; }

        [Column("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [Column("endDateTime")]
        public DateTime EndDateTime { get; set; }

        public Activity()
        {
        }

        public Activity(int activity_id, string activity_name, DateTime startDateTime, DateTime endDateTime)
        {
            ActivityId = activity_id;
            ActivityName = activity_name;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
