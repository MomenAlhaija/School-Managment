using Microsoft.EntityFrameworkCore;
using School.Core.Data;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public class MarkRepositry : IMarkRepositry
    {
        private readonly ApplicationDbContext _context;
        public MarkRepositry(ApplicationDbContext context)
        {
                _context = context; 
        }
        public void AddMark(Mark mark)
        {
            _context.Marks.Add(mark);
            _context.SaveChanges(); 
        }

        public void DeleteMark(Mark mark)
        {
            _context.Marks.Remove(mark);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Mark>> GetMarksByStudentId(int studentId)
        {
           return await _context.Marks.Where(p=>p.StudentId==studentId).ToListAsync();
        }
        public async Task<Mark?> GetMarkByIdAsync(int id)
        {
            return await _context.Marks.FindAsync(id);
        }

        public IQueryable<Mark> GetMarks()
        {
            return _context.Marks.AsQueryable();
        }

        public async Task<IQueryable<Mark>> GetMarksAsync(int id)
        {
            return _context.Marks.AsQueryable();
        }

        public async Task<decimal?> GetStudentMarkInMaterialAsyn(int materialId, int studentId)
        {
            var mark= await _context.Marks.FirstOrDefaultAsync(p=>p.MaterialId==materialId&&p.StudentId==studentId);
            if(mark!=null)
                return mark.MarkInPercent;
            return null;
        }
        public async Task<Mark?> GetMarkStudentInMaterial(int materialId, int studentId)
        {
            var mark = await _context.Marks.FirstOrDefaultAsync(p => p.MaterialId == materialId && p.StudentId == studentId);
            if (mark != null)
                return mark;
            return null;
        }

        public async Task UpdateMark(Mark mark)
        {
            _context.Update(mark);
            _context.SaveChanges();
        }

        public decimal? GetStudentMarkInMaterial(int materialId, int studentId)
        {
            var mark =  _context.Marks.FirstOrDefault(p => p.MaterialId == materialId && p.StudentId == studentId);
            if (mark != null)
                return mark.MarkInPercent;
            return null;
        }
    }
}
