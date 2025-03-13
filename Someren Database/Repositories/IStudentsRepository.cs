using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface IStudentsRepository
    {
        List<Student> ListStudents();
       
        void Add(Student user);
        void Update(Student user);
        void Delete(Student user);
    }
}
