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
    public class ClassRepositry : IClassRepositry
    {
        private readonly ApplicationDbContext _context;
        public ClassRepositry(ApplicationDbContext context)
        {
                _context= context;  
        }
        public async Task<Class> AddClass(Class input)
        {
            await _context.AddAsync(input);
           _context.SaveChanges();
           return input;
        }

        public void DeleteClass(int id)
        {
            _context.Remove(_context.Classes.Find(id));
            _context.SaveChanges();
        }

        public async Task<Class> GetClassById(int id)
        {
            var cl=await _context.Classes.FirstOrDefaultAsync(p=>p.Id==id);
            return cl;
        }

        public IQueryable<Class> GetClasses()
        {
            return _context.Classes.AsQueryable();
        }

        public void UpdateClass(Class input)
        {
            _context.Update(input);
            _context.SaveChanges();
        }
    }
}
