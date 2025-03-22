using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class DbTeachersRepository : ITeachersRepository
    {
        private readonly string? _connectionString;

        public DbTeachersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Someren_Database");
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;";

            }
            else
            {
                Console.WriteLine($"_connectionString: {_connectionString}"); 
            }
        }

        private Teacher ReadTeacher(SqlDataReader reader)
        {
            int teacherID = (int)reader["TeacherID"];
            int roomNumber = (int)reader["roomnumber"];
            string firstName = (string)reader["firstName"];
            string lastName = (string)reader["lastName"];
            string phoneNumber = (string)reader["phoneNumber"];
            int age = (int)reader["age"];


            return new Teacher(teacherID, roomNumber, firstName, lastName, phoneNumber, age);
        }

        public Teacher? GetByTeacherID(int teacherID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TeacherID, roomnumber, firstName, lastName, phoneNumber, age " +
                    "FROM Teachers WHERE TeacherID = @TeacherID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TeacherID", teacherID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Teacher teacher = ReadTeacher(reader);
                    reader.Close();
                    return teacher;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
        }

        public List<Teacher> ListTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TeacherID, RoomNumber, firstName, lastName, phoneNumber, age " +
                                "FROM Teachers ORDER BY LastName"; 
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Teacher teacher = ReadTeacher(reader);
                    teachers.Add(teacher);
                }
                reader.Close();
            }

            return teachers;
        }

        public void AddTeacher(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Teachers (TeacherID, RoomNumber, FirstName, LastName, PhoneNumber, Age) " +
                "VALUES (@TeacherID, @RoomNumber, @FirstName, @LastName, @PhoneNumber, @Age);";



                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                command.Parameters.AddWithValue("@RoomNumber", teacher.RoomNumber);
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber);
                command.Parameters.AddWithValue("@Age", teacher.Age);




                command.Connection.Open();
                int rowsChanged = command.ExecuteNonQuery();
                if (rowsChanged != 1)
                {
                    throw new Exception("Teacher addition failed");
                }

            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"UPDATE Teachers SET TeacherID = @ChangedTeacherID, FirstName = @FirstName, " +
                    $"LastName = @LastName, PhoneNumber = @PhoneNumber, age = @Age, " +
                    $"RoomNumber = @RoomNumber WHERE TeacherID = @OriginalTeacherID";

                SqlCommand command = new SqlCommand(query, connection);


                command.Parameters.AddWithValue("@ChangedTeacherID", teacher.TeacherID);
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber);
                command.Parameters.AddWithValue("@Age", teacher.Age);
                command.Parameters.AddWithValue("@RoomNumber", teacher.RoomNumber);
                command.Parameters.AddWithValue("@OriginalTeacherID", teacher.OriginalTeacherID);

                command.Connection.Open();

                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No teachers updated");
            }
        }


        public void DeleteTeacher(Teacher teacher)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "Delete Teachers WHERE TeacherID = @TeacherID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);

                connection.Open();

                int nrOfRowsAffected = command.ExecuteNonQuery();
                if (nrOfRowsAffected == 0)
                    throw new Exception("No teachers deleted");
            }
        }

        
    }
}