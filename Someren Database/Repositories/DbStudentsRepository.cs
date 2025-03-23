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

        public Student? GetByStudentNumber(int studentNumber)
        {
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT StudentNumber, FirstName, LastName, PhoneNumber, StudentClass, RoomNumber " +
                    "FROM Students WHERE StudentNumber = @StudentNumber;";

				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@StudentNumber", studentNumber);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					Student student = ReadStudent(reader); 
					reader.Close();
					return student;
				}
				else
				{
					reader.Close();
					return null;
				}
			}
		}

		public List <Student> ListStudents(string lastNameFilter = null)
        {
			
            List <Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT StudentNumber, FirstName, LastName, PhoneNumber, StudentClass, " +
								"RoomNumber FROM Students WHERE IsDeleted = 0";

                if (!string.IsNullOrEmpty(lastNameFilter))
                {
                    query += " AND LastName = @LastName";
                }

				query += " ORDER BY LastName";


				SqlCommand command = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(lastNameFilter))
                {
                    command.Parameters.AddWithValue("@LastName", lastNameFilter);
                }

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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO Students (StudentNumber, FirstName, LastName, PhoneNumber, " +
                                $" StudentClass, RoomNumber) VALUES (@StudentNumber, @FirstName, @LastName, " +
                                $"@PhoneNumber, @StudentClass, @RoomNumber);";

                SqlCommand command = new SqlCommand (query, connection);

				command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);
				command.Parameters.AddWithValue("@FirstName", student.FirstName);
				command.Parameters.AddWithValue("@LastName", student.LastName);
				command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
				command.Parameters.AddWithValue("@StudentClass", student.StudentClass);
				command.Parameters.AddWithValue("@RoomNumber", student.RoomNumber);

				command.Connection.Open();
                int rowsChanged = command.ExecuteNonQuery();
                if (rowsChanged != 1)
                {
                    throw new Exception("Student addition failed");
                }

            }
		}

        public void UpdateStudent(Student student)
        {
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = $"UPDATE Students SET StudentNumber = @ChangedStudentNumber, FirstName = @FirstName, " +
					$"LastName = @LastName, PhoneNumber = @PhoneNumber, StudentClass = @StudentClass, " +
					$"RoomNumber = @RoomNumber WHERE StudentNumber = @OriginalStudentNumber";

				SqlCommand command = new SqlCommand(query, connection);

				//for the injection thingy
				command.Parameters.AddWithValue("@ChangedStudentNumber", student.StudentNumber);
				command.Parameters.AddWithValue("@FirstName", student.FirstName);
				command.Parameters.AddWithValue("@LastName", student.LastName);
				command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
				command.Parameters.AddWithValue("@StudentClass", student.StudentClass);
				command.Parameters.AddWithValue("@RoomNumber", student.RoomNumber);
				command.Parameters.AddWithValue("@OriginalStudentNumber", student.OriginalStudentNumber);

				command.Connection.Open();

				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected == 0)
					throw new Exception("No students updated");
			}
		}
 
		public void DeleteStudent(Student student) //soft delete, I want to keep the records in the database
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "UPDATE Students SET IsDeleted = 1 WHERE StudentNumber = @StudentNumber";
				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@StudentNumber", student.StudentNumber);

				connection.Open();

				int nrOfRowsAffected = command.ExecuteNonQuery();
				if (nrOfRowsAffected == 0)
					throw new Exception("No students deleted");
			}
		}
	}
}
