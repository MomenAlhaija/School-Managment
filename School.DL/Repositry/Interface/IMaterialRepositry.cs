using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DL.Repositry
{
    public interface IMaterialRepositry
    {
        IQueryable<Material> GetMaterials();
        Material GetMaterial(int id);
        Task<Material?> GetMaterialAsync(int id);
        bool DeleteMaterial(int id);
        bool UpdateMaterial(Material material);
        Material AddMaterial(Material material);
    }
}
