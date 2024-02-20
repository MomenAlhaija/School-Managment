using Microsoft.AspNetCore.Mvc;
using School.BL.DTO;
using School.BL.Service;
using School.Core.Entities;
using SchoolPro.shared.Consts;

namespace School.Controllers
{
    public class TeacherClassController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMarkService _markService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TeacherClassController(
            ITeacherService teacherService,
            IMarkService markService,
            IHttpContextAccessor httpContextAccessor)
        {
            _teacherService = teacherService;
            _markService = markService;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            var userId=_httpContextAccessor?.HttpContext?.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var classes = await _teacherService.GetTeacherClasses((int)userId!.Value);
                return View(classes.AsEnumerable());
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> StudentsInClass(int classId,int? materialId)
        {
            var students = await _teacherService.GetStudentsClass(classId,materialId.Value);
            ViewBag.MaterialId=materialId;
            ViewBag.ClassId=classId;
            return View(students);
        }
        [HttpPost]
        public async Task UpdateGrade(StudentGradeDTO mark)
        {
            if(mark.Grade >(decimal)PersonConsts.MinGrade && mark.Grade<=(decimal)(PersonConsts.MaxGrade)) 
              await _markService.InsertMark(mark);
        }
    }
}
