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
    public class StudentRepositry : IStudentRepositry
    {
        private readonly ApplicationDbContext _context;
        public StudentRepositry(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges(); 
        }

        public async Task<Student> GetStudent(int id)
        {
            var student =await _context.Students.Include(p=>p.User).FirstOrDefaultAsync(p=>p.StudentId==id);
            if (student != null)
                return student;
            throw new Exception("Not foud the student");
        }

        public IQueryable<Student> GetStudents()
        {
            return _context.Students.Include(u=>u.User).AsQueryable();
        }

        public void UpdateStudent(Student student)
        {
            _context.Update(student);
            _context.SaveChanges();
        }
     
        public async Task DeleteStudent(Student student)
        {
           _context.Students.Remove(student);
           _context.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
        public async Task<Student?> GetStudentByUserId(int userId)
        {
            return await _context.Students.FirstOrDefaultAsync(p => p.UserId == userId);
        }
        public async Task<IEnumerable<Student?>?> GetStudentsByClassId(int classId)
        {
            var students = _context.Students.Where(p => p.ClassId == classId);
            return students;
        }
    }
}
