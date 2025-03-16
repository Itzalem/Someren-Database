using Microsoft.Data.SqlClient;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class DbStudentsRepository : IStudentsRepository
    {
        private readonly string? _connectionString;

        public DbStudentsRepository(IConfiguration configuration)
        {
			_connectionString = configuration.GetConnectionString("Someren_Database");
			if (string.IsNullOrEmpty(_connectionString))
			{
				_connectionString = "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;";
				Console.WriteLine($"_connectionString asignado manualmente: {_connectionString}");
			}
			else
			{
				Console.WriteLine($"_connectionString desde configuración: {_connectionString}");
			}
		}

        private Student ReadStudent(SqlDataReader reader)
        {
            int studentNumber = (int)reader["StudentNumber"];
            string firstName = (string)reader["FirstName"];
            string lastName = (string)reader["LastName"];
            int phoneNumber = (int)reader["PhoneNumber"];
            string studentClass = (string)reader["StudentClass"];
            int roomNumber = (int)reader["RoomNumber"];

            return new Student(studentNumber, firstName, lastName, phoneNumber, studentClass, roomNumber);
        }


		public List <Student> ListStudents()
        {
            List <Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT StudentNumber, FirstName, LastName, PhoneNumber, StudentClass, " +
                                "RoomNumber FROM Students ORDER BY LastName"; // WHERE IsDeleted = 0;"; aun no tengo soft delete
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = ReadStudent(reader);
                    students.Add(student);
                }
                reader.Close();
            }

            return students;
        }

        public void AddStudent(Student student)
        {

        }

        public void UpdateStudent(Student student)
        {

        }

        public void DeleteStudent(Student student) //soft delete
        {

        }
    }
}
