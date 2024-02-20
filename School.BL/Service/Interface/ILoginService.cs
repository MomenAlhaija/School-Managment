using School.BL.DTO;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public interface ILoginService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string passwordAttempt);
        Task<bool> AuthenticateUser(string email, string password);
        void StoreUserIdInSession(User user);
        int? GetUserTypeFromSession();
        int? GetUserIdFromSession();
        Task<bool> ResetPassword(ResetPasswordDTO password);
        void Logout();
    }
}
