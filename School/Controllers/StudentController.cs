using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using School.BL.DTO;
using School.BL.Service;
using School.DL.Repositry;

namespace School.Controllers
{
    public class StudentController : Controller
    {
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;
        public StudentController(IClassService classService,IStudentService studentService)
        {
            _classService = classService;
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            var students =await _studentService.GetAllStudent();
            return View(students);
        }
        public async Task<IActionResult> RegisterStudent()
        {
            var model = new RegisterStudentDTO();
            model.ClassItems = await GetClassesSelectListItems();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentDTO student)
        {
            try
            {
                await _studentService.RegisterStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                student.ClassItems = await GetClassesSelectListItems();
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }
        public async Task<IActionResult> EditStudent(int id)
        {
            var student =await _studentService.GetStudent(id);
            student.ClassItems = await GetClassesSelectListItems();
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(RegisterStudentDTO student)
        {
            try
            {
                await _studentService.EditStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                student.ClassItems = await GetClassesSelectListItems();
                ViewBag.ErrorMessage = ex.Message;
                return View(student);
            }
        }
        public async Task Delete(int id)
        {
            await _studentService.DeleteStudent(id);
        }
        #region Private Method
        private async Task<List<SelectListItem>> GetClassesSelectListItems()
        {
            var classes = await _classService.GetAllClasses();
            return classes.Select(classItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = classItem.ClassNameAr,
                Value = classItem.Id.ToString()
            }).ToList();
        }
        #endregion
    }
}
