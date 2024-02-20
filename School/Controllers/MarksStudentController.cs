using Microsoft.AspNetCore.Mvc;
using School.BL.Service;

namespace School.Controllers
{
    public class MarksStudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IHttpContextAccessor _httpContextAccessor;   
        public MarksStudentController(IStudentService studentService,IHttpContextAccessor httpContextAccessor)
        {
            _studentService = studentService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var userId= _httpContextAccessor?.HttpContext?.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var marks = await _studentService.GetMarksForStudent((int)userId!.Value);
                return View(marks);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
