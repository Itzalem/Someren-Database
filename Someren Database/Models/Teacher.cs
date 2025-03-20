namespace Someren_Database.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public int RoomNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

     
        public int OriginalTeacherID { get; set; }


        public Teacher()
        {
        }

        public Teacher(int teacherID, int roomNumber, string firstName, string lastName, string phoneNumber,
            int age)
        {
            TeacherID = teacherID;
            RoomNumber = roomNumber;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Age = age;
            OriginalTeacherID = teacherID;
        }
    }
}
