using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.BL.DTO;
using School.BL.Enum;
using School.Core.Entities;
using School.DL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.BL.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositry _studentRepositry;
        private readonly IUserRepositrty _userRepositrty;
        private readonly IClassRepositry _classRepositry;
        private readonly IClassMaterialRepositry _classMaterialRepositry;
        private readonly IMarkRepositry _markRepositry;
        private readonly ILoginService _loginService;
        public StudentService(IStudentRepositry studentRepositry,
                              IUserRepositrty userRepositrty,
                              IClassRepositry classRepositry,
                              IMarkRepositry markRepositry,
                              ILoginService loginService,
                              IClassMaterialRepositry classMaterialRepositry)
        {
            _studentRepositry = studentRepositry;
            _userRepositrty = userRepositrty;
            _classRepositry = classRepositry;
            _markRepositry = markRepositry;
            _classMaterialRepositry = classMaterialRepositry;
            _loginService = loginService;
        }
        public async Task<IEnumerable<RegisterStudentDTO>> GetAllStudent()
        {
            return _studentRepositry.GetStudents()
             .ToList().Select(student => new RegisterStudentDTO
             {
                 Id = student.StudentId,
                 UserId = student.UserId,
                 Email = student.User.UserName,
                 Password = student.User.Password,
                 FullNameAr = student.FullNameAr,
                 FullNameEn = student.FullNameEn,
                 Age = student.Age,
                 Gender = student.Gender,
                 ClassId = student.ClassId,
                 ClassName = _classRepositry.GetClassById(student.ClassId).Result.ClassNameAr
             }).AsEnumerable();
        }
        public async Task RegisterStudent(RegisterStudentDTO student)
        {
            try 
            { 
                var Users = _userRepositrty.GetAllUsers();
                if (Users.Any(t => t.UserName == student.Email))
                {
                    throw new Exception("The Email is Already Taken");
                }
                var hashPassword = _loginService.HashPassword(student.Password);
                var user = await _userRepositrty.AddUserAsync(new User
                {
                    Password = hashPassword,
                    UserName = student.Email,
                    FullName = student.FullNameAr,
                    UserType = (int)UserType.Student,
                });
                if (user is User && user != null)
                {
                    var studentToAdd = new Student
                    {
                        UserId = user.UserId,
                        ClassId = student.ClassId,
                        Gender = student.Gender,
                        FullNameAr = student.FullNameAr,
                        FullNameEn = student.FullNameEn,
                        Age=student.Age,
                    };
                    await _studentRepositry.AddStudent(studentToAdd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<RegisterStudentDTO> GetStudent(int id)
        {
            try
            {
                var student = await _studentRepositry.GetStudent(id);
                if (student != null)
                {
                    return new RegisterStudentDTO
                    {
                        Id = student.StudentId,
                        UserId = student.UserId,
                        Email = student.User.UserName,
                        Password = student.User.Password,
                        FullNameAr = student.FullNameAr,
                        FullNameEn = student.FullNameEn,
                        Age = student.Age,
                        Gender = student.Gender,
                        ClassId = student.ClassId,
                        ClassName = _classRepositry.GetClassById(student.ClassId).Result.ClassNameAr
                    };
                }
                else
                {
                    throw new Exception("Not Found The Student");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task EditStudent(RegisterStudentDTO student)
        {
            try
            {
                var Users = _userRepositrty.GetAllUsers();
                if (Users.Any(t => t.UserName == student.Email && t.UserId != student.UserId))
                {
                    throw new Exception("The Email is Already Taken");
                }
                var stusentFromDb = await _studentRepositry.GetStudent(student.Id.Value);
                var user = _userRepositrty.GetUserById(student.UserId.Value);
                if (stusentFromDb != null)
                {
                    stusentFromDb.Age = student.Age;
                    stusentFromDb.Gender = student.Gender;
                    stusentFromDb.ClassId = student.ClassId;
                    stusentFromDb.Gender = student.Gender;
                    stusentFromDb.FullNameAr = student.FullNameAr;
                    stusentFromDb.FullNameEn = student.FullNameEn;
                }
                else
                    throw new Exception("Not Found The Student");
                if (user != null)
                {
                    user.UserName = student.Email;
                    user.FullName = student.FullNameAr;
                }
                else
                    throw new Exception("Not Found The User");
                if (user != null && stusentFromDb != null)
                {
                    _studentRepositry.UpdateStudent(stusentFromDb);
                    _userRepositrty.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteStudent(int id)
        {
            try
            {
                var student = await _studentRepositry.GetStudent(id);
                var userId = student.UserId;

                if (student != null)
                {
                    var marks = await _markRepositry.GetMarksByStudentId(student.StudentId);
                    foreach(var mark in marks)
                    {
                        _markRepositry.DeleteMark(mark);
                    }
                    _studentRepositry.DeleteStudent(id);
                }
                else
                    throw new Exception("Not Found The student");
                if (userId != 0)
                   await _userRepositrty.DeleteUser(userId);
                else
                    throw new Exception("Not Found the User");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<MarksStudentDTO?> GetMarksForStudent(int userid)
        {
            var student = await _studentRepositry.GetStudentByUserId(userid);
            var user = _userRepositrty.GetUserById(userid);
            if(student!= null)
            {
                var classId=student.ClassId;
                var materials=await _classMaterialRepositry.GetMaterialsByClassIdAsync(classId);
                List<GradeDTO> grades = new List<GradeDTO>();
                foreach(var material in materials.ToList()) {

                    grades.Add(new GradeDTO
                    {
                        MaterialId = material.Id,
                        MaterialNameAr = material.MaterialNameAr,
                        MaterialNameEn = material.MaterialNameEn,
                        Grade =_markRepositry.GetStudentMarkInMaterial(material.Id, student.StudentId)

                    });
                }
                var marksStudent = new MarksStudentDTO()
                {
                    StudentId=student.StudentId, 
                    ClassId=classId,
                    FullNameAr=student?.FullNameAr,
                    FullNameEn=student?.FullNameEn,
                    Email=user.UserName,
                    Gender=(Gender)student.Gender,
                    Age=student.Age,
                    ClassName=_classRepositry.GetClassById(classId).Result.ClassNameAr,
                    Grades=grades.ToList()
                };
                return marksStudent;
            }
            return null;
        }
    }
}
