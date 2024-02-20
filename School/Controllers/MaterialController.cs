using Microsoft.AspNetCore.Mvc;
using School.BL.DTO;
using School.BL.Service;
using School.Core.Entities;
using School.DL.Repositry;

namespace School.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IClassRepositry _classRepositry;
        private readonly IMaterialService _materialService; 
        public MaterialController(IClassRepositry classRepositry,IMaterialService materialService)
        {
                _classRepositry=classRepositry;
                _materialService=materialService;   
        }
        public async Task<IActionResult> Index()
        {
            var materials =await _materialService.GetAllMaterials();
            return View(materials.ToList());
        }
        public IActionResult AddMaterial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMaterial(AddOrUpdateMaterialDTO input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input.MaterialNameAr))
                {
                    await _materialService.AddMaterial(input);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(input);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(input);

            }
        }
        public async Task<IActionResult> EditMaterial(int id)
        {
            var material = await _materialService.GetMaterialById(id);
            return View(material);
        }
        [HttpPost]
        public async Task<IActionResult> EditMaterial(AddOrUpdateMaterialDTO materialDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(materialDTO.MaterialNameAr))
                {
                    await _materialService.UpdateMaterial(materialDTO);
                    return RedirectToAction(nameof(Index));
                }
                return View(materialDTO);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(materialDTO);

            }
        }
        public  void Delete(int id)
        {
            _materialService.DeleteMaterial(id);
        }

    }
}
