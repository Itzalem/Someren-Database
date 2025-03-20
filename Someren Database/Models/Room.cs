using System.ComponentModel.DataAnnotations;

namespace Someren_Database.Models
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }
        
        [Required]
        public string RoomType { get; set; }

        [Required]
        public string Building { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Size { get; set; }
    }
}
