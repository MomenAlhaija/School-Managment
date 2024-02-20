using Microsoft.EntityFrameworkCore;
using School.Core.Data;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public class ClassMaterialRepositry : IClassMaterialRepositry
    {
        private readonly ApplicationDbContext _context;
        public ClassMaterialRepositry(ApplicationDbContext context)
        {
                _context=context;
        }
        public bool AddClassMaterial(ClassMaterial classMaterial)
        {
           _context.ClassMaterials.Add(classMaterial);
           int rowAffect= _context.SaveChanges();
           return rowAffect > 0;
        }

        public IQueryable<Class> GetClassMaterialsByMaterialId(int id)
        {
            return
                _context.ClassMaterials
                .Where(p => p.MaterialId == id)
                .Include(p => p.Class)
                .Select(p => p.Class)
                .AsQueryable();
        }
        public IQueryable<Material> GetClassMaterialsByClassID(int id)
        {
            return
                _context.ClassMaterials
                .Where(p => p.ClassId == id)
                .Include(p => p.Material)
                .Select(p => p.Material)
                .AsQueryable();
        }
        public List<int> MaterilsIds(int classId)
        {
            return _context.ClassMaterials.ToList().Where(p=>p.ClassId==classId).Select(p => p.MaterialId).ToList();
            
        }
        public void DeleteClassMaterial(int classId)
        {
            var classes = _context.ClassMaterials.Where(p=>p.ClassId==classId);
            _context.ClassMaterials.RemoveRange(classes);
            _context.SaveChanges();
        }
        public void UpdateMaterialClasses(int classId, List<int> MaterialIds)
        {
            DeleteClassMaterial(classId);

            foreach (int materialId in MaterialIds)
            {
                AddClassMaterial(new ClassMaterial
                {
                    ClassId = classId,
                    MaterialId = materialId
                });
            }
        }
        public bool UpdateClassMaterial(ClassMaterial classMaterial)
        {
            _context.Update(classMaterial);
            int rowAffected = _context.SaveChanges();
            return rowAffected > 0;
        }
        public IEnumerable<Class?> GetClassesByMaterialId(int materialId)
        {
            var classMaterials= _context.ClassMaterials.Where(p=>p.MaterialId==materialId);
            var classes = classMaterials.Select(p => p.Class);
            return classes;
        }
        public async Task<IEnumerable<Class?>> GetClassesByMaterialIdAsync(int materialId)
        {
            var classMaterials = _context.ClassMaterials.Where(p => p.MaterialId == materialId);
            var classes = classMaterials.Select(p => p.Class);
            return classes;
        }

        public async Task<IQueryable<Material>> GetMaterialsByClassIdAsync(int classId)
        {
           return  _context.ClassMaterials.Where(p => p.ClassId == classId)
                .Select(p=>p.Material).AsQueryable();
        }
    }
}
