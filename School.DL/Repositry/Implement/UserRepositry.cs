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
    public class UserRepositry : IUserRepositrty
    {
        private readonly ApplicationDbContext _context;
        public UserRepositry(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public User? AddUser(User user)
        {
            var obj = _context.Add(user);
            _context.SaveChanges();
            return obj.Entity;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            var obj=  _context.Add(user);
            _context.SaveChanges();
            return obj.Entity;
        }

        public async Task DeleteUser(int id)
        {
            var user =await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
            else
                throw new Exception("Not Found The User");
        }

        public IQueryable<User> GetAllUsers()
        {
           return _context.Users.AsQueryable();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id)!;
            
        }

        public  void UpdateUser(User user)
        {
             _context.Users.Update(user);
            _context.SaveChanges();

        }
        public async Task<User?> FindUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserName == email);
        }

      
    }
}
