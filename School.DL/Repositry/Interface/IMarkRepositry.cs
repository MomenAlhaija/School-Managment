using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IMarkRepositry
    {
        Task<IQueryable<Mark>> GetMarksAsync(int id);
        IQueryable<Mark> GetMarks();
        Task<Mark> GetMarkByIdAsync(int id);
        void AddMark(Mark mark);
        void DeleteMark(Mark mark);
        Task<IEnumerable<Mark>> GetMarksByStudentId(int studentId);
        Task UpdateMark(Mark mark);

        Task<decimal?> GetStudentMarkInMaterialAsyn(int materialId, int studentId);
        decimal? GetStudentMarkInMaterial(int materialId, int studentId);

        Task<Mark?> GetMarkStudentInMaterial(int materialId, int studentId);

    }
}
