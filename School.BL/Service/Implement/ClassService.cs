using School.BL.DTO;
using School.DL.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Core.Entities;
namespace School.BL.Service
{
    public class ClassService : IClassService
    {
        private readonly IClassRepositry _classRepositry;
        private readonly IClassMaterialRepositry _classMaterialRepo;
        public ClassService(IClassRepositry classRepositry, IClassMaterialRepositry classMaterialRepo)
        {
            _classRepositry = classRepositry;
            _classMaterialRepo = classMaterialRepo;

        }
        public async Task AddClass(ClassDTO input) 
        {
            try
            {
                var classes = await GetAllClasses();
                if (classes.Any(p => p!.ClassNameEn!.ToUpper().Equals(input!.ClassNameEn!.ToUpper()!) &&
                               p.ClassNameAr!.ToUpper().Equals(input!.ClassNameAr!.ToUpper())))
                {
                    throw new Exception("the Class Is Already Found");
                }
                Class classToAdd = await _classRepositry.AddClass(new Class
                {
                    ClassNameAr = input.ClassNameAr,
                    ClassNameEn = input.ClassNameEn,
                });
                if (classToAdd != null)
                {
                    foreach (int materialId in input.MaterialsIds)
                    {
                        _classMaterialRepo.AddClassMaterial(new ClassMaterial
                        {
                            ClassId = classToAdd.Id,
                            MaterialId = materialId,
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async void DeleteClassById(int id)
        {
            try
            {
                _classMaterialRepo.DeleteClassMaterial(id);
                _classRepositry.DeleteClass(id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  async  Task<IQueryable<ClassDTO>> GetAllClasses()
        {
            var classes= _classRepositry.GetClasses();
            return classes.Select(cl => new ClassDTO
            {
                Id=cl.Id,
                ClassNameAr = cl.ClassNameAr,
                ClassNameEn = cl.ClassNameEn,
                Materials = _classMaterialRepo.GetClassMaterialsByClassID(cl.Id).AsEnumerable()
            });
        }

        public async Task<ClassDTO> GetClassById(int id)
        {
            try
            {
                var classFromDb = await _classRepositry.GetClassById(id);
                if (classFromDb != null && id == classFromDb.Id)
                {
                    return new ClassDTO
                    {
                        Id = classFromDb.Id,
                        ClassNameAr = classFromDb.ClassNameAr,
                        ClassNameEn = classFromDb.ClassNameEn,
                        MaterialsIds = _classMaterialRepo.MaterilsIds(classFromDb.Id),
                        Materials = _classMaterialRepo.GetClassMaterialsByClassID(classFromDb.Id).AsEnumerable()
                    };
                }
                else
                    throw new Exception("Not Found The Class");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateClass(ClassDTO input)
        {
            try
            {
                var classes = await GetAllClasses();
                if (classes.Any(p => p.ClassNameEn!.ToUpper()!.Equals(input!.ClassNameEn!.ToUpper()) &&
                               p.ClassNameAr!.ToUpper()!.Equals(input!.ClassNameAr!.ToUpper()) &&
                               p.Id != input.Id))
                {
                    throw new Exception("the Class Is Already Found");
                }
                var classFromDb = await _classRepositry.GetClassById(input.Id.Value);
                if (classFromDb != null && classFromDb.Id == input.Id.Value)
                {

                    classFromDb.ClassNameAr = input.ClassNameAr;
                    classFromDb.ClassNameEn = input.ClassNameEn;
                    _classRepositry.UpdateClass(classFromDb);
                    _classMaterialRepo.UpdateMaterialClasses(input.Id.Value, input.MaterialsIds);
                }
                else
                    throw new Exception("Not Found The Class");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
