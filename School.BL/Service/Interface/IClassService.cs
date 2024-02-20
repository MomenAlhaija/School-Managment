using School.BL.DTO;

namespace School.BL.Service
{
    public interface IClassService
    {
        Task AddClass(ClassDTO input);
        Task<IQueryable<ClassDTO>> GetAllClasses();
        Task<ClassDTO> GetClassById(int id);
        void DeleteClassById(int id);
        Task UpdateClass(ClassDTO input);
    }
}
