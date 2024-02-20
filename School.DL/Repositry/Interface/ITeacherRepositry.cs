using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface ITeacherRepositry
    {
        Task AddTeacher(Teacher teacher);
        IQueryable<Teacher> GetTeachers();
        Task<Teacher> GetTeacher(int id);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);
        Task DeleteTeacher(Teacher teacher);
        Task<Teacher?> GetTeacherByUserId(int userId);
    }
}
