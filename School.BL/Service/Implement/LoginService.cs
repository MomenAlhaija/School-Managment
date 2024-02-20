using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using School.BL.DTO;
using School.Core.Entities;
using School.DL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepositrty _userRepositrty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(IUserRepositrty userRepositrty,
                            IHttpContextAccessor httpContextAccessor)
        {
            _userRepositrty=userRepositrty;
            _httpContextAccessor = httpContextAccessor;
        }
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            string combinedHash = $"$HASHED${Convert.ToBase64String(salt)}${hashedPassword}";

            return combinedHash;
        }
        public bool VerifyPassword(string hashedPassword, string passwordAttempt)
        {
            string[] hashParts = hashedPassword.Split('$');
            if (hashParts.Length != 4 || hashParts[1] != "HASHED")
            {
                throw new FormatException("Invalid hashed password format.");
            }

            byte[] salt = Convert.FromBase64String(hashParts[2]);
            string storedHash = hashParts[3];

            string hashedPasswordAttempt = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordAttempt,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return storedHash.Equals(hashedPasswordAttempt);
        }
        public async Task<bool> AuthenticateUser(string email, string password)
        {
            var user = await _userRepositrty.FindUserByEmail(email);
            if (user == null)
                return false;
            if (VerifyPassword(user.Password, password))
            {
                StoreUserIdInSession(user);
                return true;
            }
            return false;
        }
        public void StoreUserIdInSession(User user)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.UserId);
            _httpContextAccessor.HttpContext.Session.SetInt32("UserType", user.UserType);
        }
        public int? GetUserIdFromSession()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
        }
        public int? GetUserTypeFromSession()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("UserType");
        }
        
        public async Task<bool> ResetPassword(ResetPasswordDTO password)
        {
            try
            {
                var userId = GetUserIdFromSession();
                if ((int)(userId!.Value) == password.UserId)
                {
                    var user = _userRepositrty.GetUserById(userId.Value);
                    if (user != null)
                    {
                        if (VerifyPassword(user.Password, password.OldPassword))
                        {
                            user.Password = HashPassword(password.NewPassword);
                            return true;
                        }
                        else
                            throw new Exception("Invalid Password");
                    }
                    else
                        throw new Exception("Can't Find The User");
                }
                else
                    throw new Exception("You are not Authenticated Please Login To Reset Your Password");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

    }
}
