using School.BL.DTO;
using School.BL.Enum;
using School.Core.Data;
using School.Core.Entities;
using School.DL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepositrty _userRepositry;
        private readonly IStudentService _studentService;
        private readonly ITeacherRepositry _teacherRepositry;
        private readonly ILoginService _loginService;
        private readonly IStudentRepositry _studentRepositry;
        private readonly ITeacherService _teacherService;
        public UserService(
            IUserRepositrty userRepositrty, 
            IStudentService studentService, 
            ITeacherRepositry teacherRepositry,
            ILoginService loginService,
            IStudentRepositry studentRepositry,
            ITeacherService teacherService)
        {
            _userRepositry = userRepositrty;
            _studentService = studentService;
            _teacherRepositry = teacherRepositry;
            _loginService = loginService;
            _studentRepositry = studentRepositry;
            _teacherService = teacherService;

        }

        public async Task AddUser(UserDTO user)
        {
            try
            {
                var users = await GetUsers();
                if (users.Any(p => p.UserName == user.UserName))
                    throw new Exception("The Email is Already Taken");
                else
                    await _userRepositry.AddUserAsync(new User
                    {
                        FullName = user.FullName,
                        Password =_loginService.HashPassword(user.Password),
                        UserName = user.UserName,
                        UserType = user.UserType
                    });
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return _userRepositry.GetAllUsers().Select(p => new UserDTO
            {
                UserId=p.UserId,
                Password=p.Password,
                UserName=p.UserName,
                FullName=p.FullName,
                UserType=p.UserType
            }).AsEnumerable();
        }

        public async Task UpdateUser(UserDTO user)
        {
            try
            {
                var users = await GetUsers();
                if (users.Any(p => p.UserName == user.UserName && p.UserId != user.UserId))
                    throw new Exception("The Email is Already Taken");
                var userFromDb = _userRepositry.GetUserById(user.UserId.Value);
                if (userFromDb != null && user.UserId==userFromDb.UserId)
                {
                    userFromDb.UserName = user.UserName;
                    userFromDb.FullName = user.FullName;
                    userFromDb.UserType = user.UserType;
                    _userRepositry.UpdateUser(userFromDb);
                }
                else
                    throw new Exception("Not Found The User");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
               var user=GetUserById(id);
               if (user.UserType == (int)(UserType.Student))
               {
                    var student = await _studentRepositry.GetStudentByUserId(user.UserId.Value);
                    if (student != null)
                    {
                        await _studentService.DeleteStudent(student.StudentId);
                    }
               }
               else if(user.UserType== (int)(UserType.Teacher))
               {
                   var teacher=await _teacherRepositry.GetTeacherByUserId(id);
                    if (teacher != null)
                    {
                        await _teacherService.DeleteTeacher(teacher.TeacherId);
                    }
               }
               _userRepositry.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  UserDTO GetUserById(int id)
        {
            try
            {
                var user = _userRepositry.GetUserById(id);
                if (user != null && id == user.UserId)
                {
                    return new UserDTO
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Password = user.Password,
                        FullName = user.FullName,
                        UserType = user.UserType
                    };
                }
                else
                    throw new Exception("Not Found The User");
            }
            catch(Exception ex)
            { throw new Exception(ex.Message); }
        }
        public async Task<bool> ResetPassword(ResetPasswordDTO password)
        {
            try
            {
                var user = _userRepositry.GetUserById(password.UserId);
                if (user != null && user.UserId == password.UserId)
                {
                    if (_loginService.VerifyPassword(user.Password, password.OldPassword))
                    {
                        user.Password = _loginService.HashPassword(password.NewPassword);
                        _userRepositry.UpdateUser(user);
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            { throw ex;}
        }

        public async Task<UserDTO> Login(LoginDTO login)
        {
            try
            {
                var user = await _userRepositry.FindUserByEmail(login.Email);
                if(await _loginService.AuthenticateUser(login.Email,login.Password))
                        return new UserDTO
                        {
                            UserId = user!.UserId,
                            FullName = user.FullName,
                            UserName = user.UserName,
                            Password = user.Password,
                            UserType=user.UserType
                        };
                throw new Exception("The Email Or Password is Incorrect");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
      
    }
        
}

