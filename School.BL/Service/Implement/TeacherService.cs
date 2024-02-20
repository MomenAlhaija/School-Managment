using School.BL.DTO;
using School.BL.Enum;
using School.Core.Entities;
using School.DL.Repositry;

namespace School.BL.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepositry _teacherRepositry;
        private readonly IUserRepositrty _userRepositrty;
        private readonly IMaterialRepositry _materialRepositry;
        private readonly IClassMaterialRepositry _classMaterialRepositry;
        private readonly IStudentRepositry _studentRepositry;
        private readonly IMarkRepositry _markRepositry;
        private readonly ILoginService _loginService;
        public TeacherService(
            ITeacherRepositry teacherRepositry, 
            IUserRepositrty userRepositrty,
            IMaterialRepositry materialRepositry,
            IClassMaterialRepositry classMaterialRepositry,
            IStudentRepositry studentRepositry,
            IMarkRepositry markRepositry,ILoginService loginService)
        {
            _teacherRepositry = teacherRepositry;
            _userRepositrty = userRepositrty;
            _materialRepositry = materialRepositry;
            _classMaterialRepositry = classMaterialRepositry;
            _studentRepositry = studentRepositry;
            _markRepositry = markRepositry;
            _loginService = loginService;
        }
        private async Task<bool> TheMaterialIsAssignedToTeacher(int materialId)
        {
            var teachers =await GetAllTeacher();
            return teachers.Any(p => p.MaterialId == materialId);      
        }
        public async Task AddTeacher(RegisterTeacherDTO teacher)
        {
            try
            {
                if (! await TheMaterialIsAssignedToTeacher(teacher.MaterialId))
                {
                    var Users = _userRepositrty.GetAllUsers();
                    if (Users.Any(t => t.UserName == teacher.Email))
                    {
                        throw new Exception("The Email is Already Taken");
                    }
                    var user = await _userRepositrty.AddUserAsync(new User
                    {
                        Password = _loginService.HashPassword(teacher.Password),
                        UserName = teacher.Email,
                        FullName = teacher.FullNameAr,
                        UserType = (int)UserType.Teacher,
                    });
                    if (user is User && user != null)
                    {
                        var teacherToAdd = new Teacher
                        {
                            UserId = user.UserId,
                            MaterialId = teacher.MaterialId,
                            Gender = teacher.Gender,
                            FullNameAr = teacher.FullNameAr,
                            FullNameEn = teacher.FullNameEn,
                        };
                        await _teacherRepositry.AddTeacher(teacherToAdd);
                    }
                }
                else
                {
                    throw new Exception("This Material Is Already Assigned To Teacher");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<RegisterTeacherDTO>> GetAllTeacher()
        {
            try
            {
                var teachers = _teacherRepositry.GetTeachers().ToList();
                return teachers.Select(teacher => new RegisterTeacherDTO
                {
                    Id = teacher.TeacherId,
                    UserId = teacher.UserId,
                    Email = teacher.User.UserName,
                    Password = teacher.User.Password,
                    FullNameAr = teacher.FullNameAr,
                    FullNameEn = teacher.FullNameEn,
                    Gender = teacher.Gender,
                    MaterialId = teacher.MaterialId,
                    MaterialName = _materialRepositry.GetMaterial(teacher.MaterialId)?.MaterialNameAr // Fetch material separately
                }).AsEnumerable();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RegisterTeacherDTO> GetTeacher(int id)
        {
            try
            {
                var teacher = await _teacherRepositry.GetTeacher(id);
                if (teacher != null && teacher.TeacherId==id)
                    return new RegisterTeacherDTO
                    {
                        Id = teacher.TeacherId,
                        UserId = teacher.UserId,
                        Email = teacher.User.UserName,
                        Password = teacher.User.Password,
                        FullNameAr = teacher.FullNameAr,
                        FullNameEn = teacher.FullNameEn,
                        Gender = teacher.Gender,
                        MaterialId = teacher.MaterialId,
                        MaterialName = _materialRepositry.GetMaterial(teacher.MaterialId).MaterialNameAr
                    };
                else
                    throw new Exception("Not Found The Teacher");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task EditTeacher(RegisterTeacherDTO teacher)
        {
            try
            {

                var teacherFromDb = await _teacherRepositry.GetTeacher(teacher.Id.Value);
                var user = _userRepositrty.GetUserById(teacherFromDb.UserId);
                var Users = _userRepositrty.GetAllUsers();
                var teachers = await GetAllTeacher();
                if (teachers.Any(p => p.MaterialId == teacher.MaterialId && p.Id != teacher.Id))
                    throw new Exception("This Material Is Already Assigned To Teacher");
                if (Users.Any(t => t.UserName == teacher.Email && t.UserId != teacher.UserId))
                {
                    throw new Exception("The Email is Already Taken");
                }
                if (teacherFromDb != null && teacher.Id==teacherFromDb.TeacherId)
                {
                    teacherFromDb.Gender = teacher.Gender;
                    teacherFromDb.MaterialId = teacher.MaterialId;
                    teacherFromDb.Gender = teacher.Gender;
                    teacherFromDb.FullNameAr = teacher.FullNameAr;
                    teacherFromDb.FullNameEn = teacher.FullNameEn;
                }
                else
                    throw new Exception("Not Found The Teacher");
                if (user != null && teacher.UserId==user.UserId)
                {
                    user.UserName = teacher.Email;
                    user.FullName = teacher.FullNameAr;
                }
                else
                    throw new Exception("Not Found The User");
                if (teacherFromDb != null && user != null)
                {
                    _teacherRepositry.UpdateTeacher(teacherFromDb);
                    _userRepositrty.UpdateUser(user);

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteTeacher(int id) 
        {
            try
            {
                var teacher = await GetTeacher(id);
                var userId = teacher.UserId;
                if (teacher != null && id==teacher.Id)
                {
                    _teacherRepositry.DeleteTeacher(id);
                }
                else
                    throw new Exception("Not Found The Teacher");
                if (userId != null && userId != 0 )
                {
                    _userRepositrty.DeleteUser(userId.Value);
                }
                else
                    throw new Exception("Not Found The User");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<ClassDTO?>> GetTeacherClasses(int userId)
        {
            try
            {
                var teacher = await _teacherRepositry.GetTeacherByUserId(userId);
                if (teacher != null)
                {
                    var materialId = teacher.MaterialId;
                    var classes = await _classMaterialRepositry.GetClassesByMaterialIdAsync(materialId); // Await the asynchronous method call
                    var material = await _materialRepositry.GetMaterialAsync(materialId);                    
                    return classes.Select(classItem => new ClassDTO
                    {
                        Id = classItem!.Id,
                        ClassNameAr = classItem.ClassNameAr,
                        ClassNameEn = classItem.ClassNameEn,
                        ClassMaterialForTeacher = material
                    });
                }
                else
                {
                    throw new Exception("Teacher not found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StudentDTO?>> GetStudentsClass(int classId)
        {
            var students = await _studentRepositry.GetStudentsByClassId(classId);
            List<StudentDTO> listStudents = new List<StudentDTO>();

            foreach (var item in students)
            {
                var studentDto = new StudentDTO();

                if (item != null)
                {
                    studentDto.StudentId = item.StudentId;

                    if (item.User != null)
                    {
                        studentDto.UserId = item.User.UserId;
                    }

                    studentDto.FullNameAr = item.FullNameAr;
                    studentDto.FullNameEn = item.FullNameEn;
                    studentDto.Gender = (Gender)(item.Gender);
                    studentDto.Age = item.Age;
                }

                listStudents.Add(studentDto);
            }

            return listStudents;
        }
        public async Task<IEnumerable<StudentDTO?>> GetStudentsClass(int classId, int materialId)
        {
            var students = await _studentRepositry.GetStudentsByClassId(classId); 
            students = students?.ToList();
            List<StudentDTO> listStudents = new List<StudentDTO>();

            foreach (var item in students)
            {
                var mark = await _markRepositry.GetStudentMarkInMaterialAsyn(materialId, item.StudentId);
                var studentDto = new StudentDTO();
                studentDto.StudentId = item.StudentId;
                studentDto.FullNameAr = item.FullNameAr;
                studentDto.FullNameEn = item.FullNameEn;
                studentDto.Gender = (Gender)item.Gender;
                studentDto.Age = item.Age;
                studentDto.Grade = mark;

                if (item.User != null)
                {
                    studentDto.UserId = item.User.UserId;
                }

                listStudents.Add(studentDto);
            }

            return listStudents;
        }

       
    }
}
