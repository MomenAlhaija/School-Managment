using School.BL.DTO;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public interface ITeacherService
    {
        Task AddTeacher(RegisterTeacherDTO teacher);
        Task<IEnumerable<RegisterTeacherDTO>> GetAllTeacher();
        Task<RegisterTeacherDTO> GetTeacher(int id);
        Task EditTeacher(RegisterTeacherDTO teacher);
        Task DeleteTeacher(int id);
        Task<IEnumerable<StudentDTO?>?> GetStudentsClass(int classId);
        Task<IEnumerable<StudentDTO?>?> GetStudentsClass(int classId,int materialId);
        Task<IEnumerable<ClassDTO?>?> GetTeacherClasses(int userId);
    }
}
