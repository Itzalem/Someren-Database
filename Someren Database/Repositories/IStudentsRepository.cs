using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface IStudentsRepository
    {
        List<Student> ListStudents(string lastNameFilter);
        Student GetByStudentNumber(int studentNumber);

		void AddStudent (Student user);
        void UpdateStudent (Student user);
        void DeleteStudent (Student user);
        
    }
}
