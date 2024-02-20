using Microsoft.AspNetCore.Mvc;
using School.BL.DTO;
using School.BL.Service;
using School.DL.Repositry;

namespace School.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassService _classService;
        private readonly IMaterialRepositry _materialRepositry;
        public ClassesController(IClassService classService,IMaterialRepositry materialRepositry)
        {
            _classService = classService;
            _materialRepositry = materialRepositry;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await _classService.GetAllClasses();
            return View(classes.AsEnumerable());
        }
        public IActionResult AddClass() {
            var model = new ClassDTO
            {
                MateriaLItems = _materialRepositry.GetMaterials().Select(MaterialItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = MaterialItem.MaterialNameAr,
                    Value = MaterialItem.Id.ToString()
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass(ClassDTO input)
        {

            try
            {
                input.MateriaLItems = _materialRepositry.GetMaterials().Select(MaterialItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = MaterialItem.MaterialNameAr,
                    Value = MaterialItem.Id.ToString()
                }).ToList();
                if (!String.IsNullOrEmpty(input.ClassNameAr))
                {
                    await _classService.AddClass(input);
                    return RedirectToAction(nameof(Index));
                }
                return View(input);
                
            }
            catch(Exception ex)
            {
               
                ViewBag.ErrorMessage = ex.Message;
                return View(input);

            }
        }
        public async Task<IActionResult> EditClass(int id)
        {
            var cl=await _classService.GetClassById(id);
            cl.MateriaLItems = _materialRepositry.GetMaterials().Select(MaterialItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = MaterialItem.MaterialNameAr,
                Value = MaterialItem.Id.ToString()
            }).ToList();
            return View(cl);
        }
        [HttpPost]
        public async Task<IActionResult> EditClass(ClassDTO input)
        {
            try
            {
                input.MateriaLItems = _materialRepositry.GetMaterials().Select(MaterialItem => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = MaterialItem.MaterialNameAr,
                    Value = MaterialItem.Id.ToString()
                }).ToList();
                if (!string.IsNullOrEmpty(input.ClassNameAr))
                {
                    await _classService.UpdateClass(input);
                    return RedirectToAction(nameof(Index));
                }
                return View(input);
            }
            catch (Exception ex)
            {
                
                ViewBag.ErrorMessage = ex.Message;
                return View(input);

            }
        }
        public void Delete(int id)
        {
            _classService.DeleteClassById(id);
        }
    }

}
