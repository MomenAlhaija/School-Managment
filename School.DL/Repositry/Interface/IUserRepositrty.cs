using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IUserRepositrty
    {
        IQueryable<User> GetAllUsers();
        Task<User> AddUserAsync(User user);
        User AddUser(User user);
        void UpdateUser(User user);
        User GetUserById(int id);
        Task DeleteUser(int id);
        Task<User?> FindUserByEmail(string email);
    }
}
