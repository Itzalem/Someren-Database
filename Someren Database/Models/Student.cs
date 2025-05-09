﻿namespace Someren_Database.Models
{
    public class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string StudentClass { get; set; }

        public int RoomNumber { get; set; }
		public int OriginalStudentNumber { get; set; }


		public Student ()
        {
        }

        public Student (int studentNumber, string firstName, string lastName, int phoneNumber, 
            string studentClass, int roomNumber)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            StudentClass = studentClass;
            RoomNumber = roomNumber;
			OriginalStudentNumber = studentNumber;
		}
    }
}
