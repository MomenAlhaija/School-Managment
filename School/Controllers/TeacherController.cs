using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BL.DTO;
using School.BL.Service;
using School.Core.Entities;
using School.DL.Repositry;

namespace School.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly ITeacherService _teacherService;
        private readonly IUserService _userService;
        public TeacherController(IMaterialService materialService,ITeacherService teacherService,IUserService userService)
        {
            _materialService = materialService; 
            _teacherService=teacherService;
            _userService = userService;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var teachers = await _teacherService.GetAllTeacher();
                return View(teachers);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();  
            }
        }
       
        public async Task<IActionResult> AddTeacher() {
            var model = new RegisterTeacherDTO();
            model.MaterialsItems =await GetSelectListMaterialsItems();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher(RegisterTeacherDTO teacher)
        {
            try
            {
                await _teacherService.AddTeacher(teacher);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                teacher.MaterialsItems = await GetSelectListMaterialsItems();
                ViewBag.ErrorMessage = ex.Message;
                return View(teacher);
            }
        }
        public async Task<IActionResult> EditTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacher(id);
            var materials = await _materialService.GetAllMaterials();
            teacher.MaterialsItems = await GetSelectListMaterialsItems(); 
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> EditTeacher(RegisterTeacherDTO teacher)
        {
            try
            {
                await _teacherService.EditTeacher(teacher);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                var materials = await _materialService.GetAllMaterials();
                teacher.MaterialsItems = await GetSelectListMaterialsItems();
                ViewBag.ErrorMessage = ex.Message;
                return View(teacher);
            }
        }
        public  async Task Delete(int id)
        {
            try
            {
                await _teacherService.DeleteTeacher(id);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

        }
        #region Private Method
        private async Task<List<SelectListItem>> GetSelectListMaterialsItems()
        {
            var materials = await _materialService.GetAllMaterials();
            return materials.Select(MaterialItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = MaterialItem.MaterialNameAr,
                Value = MaterialItem.Id.ToString()
            }).ToList();
        }
        #endregion
    }
}
