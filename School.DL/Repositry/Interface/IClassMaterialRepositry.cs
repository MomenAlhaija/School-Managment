using School.Core.Data;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IClassMaterialRepositry
    {
        bool AddClassMaterial(ClassMaterial classMaterial);
        IQueryable<Class> GetClassMaterialsByMaterialId(int id);
        bool UpdateClassMaterial(ClassMaterial classMaterial);
        void UpdateMaterialClasses(int materialId, List<int> classIds);
        void DeleteClassMaterial(int materialId);
        IQueryable<Material> GetClassMaterialsByClassID(int id);
        List<int> MaterilsIds(int classId);
        IEnumerable<Class?> GetClassesByMaterialId(int materialId);
        Task<IEnumerable<Class?>> GetClassesByMaterialIdAsync(int materialId);
        Task<IQueryable<Material>> GetMaterialsByClassIdAsync(int classId);
    }
}
