using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface ITeachersRepository
    {
        List<Teacher> ListTeachers();
        Teacher GetByTeacherID(int teacherID);

        void AddTeacher(Teacher user);
        void UpdateTeacher(Teacher user);
        void DeleteTeacher(Teacher user);



    }
}
