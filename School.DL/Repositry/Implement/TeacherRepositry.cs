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
    public class TeacherRepositry : ITeacherRepositry
    {
        private readonly ApplicationDbContext _context;
        public TeacherRepositry(ApplicationDbContext context)
        {
                _context= context;  
        }
        public async Task AddTeacher(Teacher teacher)
        {
            await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public  void DeleteTeacher(int id)
        {
             _context.Remove(_context.Teachers.Find(id));
            _context.SaveChanges();
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await _context.Teachers.Include(p=>p.User).Include(p => p.Material).FirstOrDefaultAsync(p=>p.TeacherId==id);

        }

        public IQueryable<Teacher> GetTeachers()
        {
            return _context.Teachers.Include(p=>p.User).Include(p => p.Material).AsQueryable();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            _context.SaveChanges();
        }
        public async Task<Teacher?> GetTeacherByUserId(int userId)
        {
            return await _context.Teachers.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
