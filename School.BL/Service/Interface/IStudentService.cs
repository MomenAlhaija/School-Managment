using School.BL.DTO;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public interface IStudentService
    {
        Task RegisterStudent(RegisterStudentDTO student);
        Task<IEnumerable<RegisterStudentDTO>> GetAllStudent();
        Task<RegisterStudentDTO> GetStudent(int id);
        Task EditStudent(RegisterStudentDTO student);
        Task DeleteStudent(int id);
        Task<MarksStudentDTO?> GetMarksForStudent(int userid);
    }
}
