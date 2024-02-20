using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IStudentRepositry
    {
        Task AddStudent(Student student);
        IQueryable<Student> GetStudents();
        Task<Student> GetStudent(int id);
        void UpdateStudent(Student student);
        Task DeleteStudent(Student student);
        void DeleteStudent(int id);
        Task<Student?> GetStudentByUserId(int userId);
        Task<IEnumerable<Student?>?> GetStudentsByClassId(int classId);
    }
}
